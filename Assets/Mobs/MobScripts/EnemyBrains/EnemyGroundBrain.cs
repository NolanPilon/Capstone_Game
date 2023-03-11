using Pathfinding;
using Pathfinding.Util;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGroundBrain : MonoBehaviour
{
    public EnemyGroundInfo EnemyGroundInfo;

    [Header("EnemyStats")]
    private int health;
    private int damage;


    [Header("Pathfinding")]
    private string tagTarget;
    private float activateDistance;
    private float pathUpdateSeconds;

    [Header("Physics")]
    private float nextWaypointDistance;
    private float jumpNodeHeightRequirement;
    private float minPlayerHeightDistance;
    private float jumpModifier;
    private float jumpCheckOffset;
    private float mySpeed;

    [Header("Attack Info")]
    private float attackRange;
    private float safetyRange;
    private float delayAfterAttack;
    private float delayAfterGettingHit;
    private bool isRanger;


    [Header("Custom Behavior")]
    private bool isTurret;
    private bool followEnabled;
    private bool jumpEnabled;
    private bool minJumpDistanceEnable;
    private bool directionLookEnabled;

  //  public ParticleSystem landing;
    private Path path;
    private int currentWaypoint;
    private float NextAttack;
    private bool isGrounded = false;
    private GameObject deathParticles;


    Seeker seeker;
    Rigidbody2D rb;
    PathFindingGround movement;
    RangeAttack rangeAttack;

    void Start()
    {
        StartUP();
    }

    void FixedUpdate()
    {
        if (health > 0)
        {
            if (!isTurret)
            {
                //checks if target is in agro distance and if the mob can move also added a delay if it attacks
                if (movement.TargetInDistance() && followEnabled || Time.time > NextAttack)
                {

                    // if in attack distance
                    if (movement.TargetDistance() >= attackRange)
                    {
                        movement.PathFollow();
                    }
                    // keeps the mob safe 
                    else if (movement.TargetDistance() <= safetyRange)
                    {
                        movement.RunAway();
                    }
                    //if the mob is in between safe and attack it will now attack
                    else
                    {
                        if (isRanger)
                        {
                            //delay attack
                            if (Time.time > NextAttack)
                            {
                                rangeAttack.ShootBullet();
                                NextAttack = Time.time + delayAfterAttack;
                            }

                            movement.MovementStop();
                        }

                        movement.MovementStop();
                    }

                }
            }
            else
            {
                //locks the turrent in place
                movement.MovementStop();
                 
                if (movement.TargetDistance() <= attackRange)
                {
                    //delay attack
                    if (Time.time > NextAttack)
                    {
                        rangeAttack.ShootBullet();
                        NextAttack = Time.time + delayAfterAttack;
                    }
                }
            }
        
        }
        else
        {
            // mob is dead 
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.parryCombo >= health)
        {
            StartCoroutine(AttackFreeze());
            createDeathParticles();
            
            transform.position = new Vector2(1000, 1000);
            Destroy(this.gameObject, 0.2f);
        }
        else if (collision.tag == "Player" && Time.time > NextAttack)
        {
            NextAttack = Time.time + delayAfterAttack;
        }

    }

    IEnumerator AttackFreeze()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1.0f;
    }
 
    void createDeathParticles()
    {
        Instantiate(deathParticles, gameObject.transform.position, gameObject.transform.rotation);
    }

    void StartUP()
    {
        //Sets Mob Stats
        health = EnemyGroundInfo.health;
        damage = EnemyGroundInfo.damage;


        //Sets Pathfinding Info
        tagTarget = EnemyGroundInfo.targetTag;
        activateDistance = EnemyGroundInfo.activateDistance;
        pathUpdateSeconds = EnemyGroundInfo.pathUpdateSeconds;

        //Sets Physics info
        nextWaypointDistance = EnemyGroundInfo.nextWaypointDistance;
        jumpNodeHeightRequirement = EnemyGroundInfo.jumpNodeHeightRequirement;
        minPlayerHeightDistance = EnemyGroundInfo.minPlayerHeightDistance;
        jumpModifier = EnemyGroundInfo.jumpModifier;
        jumpCheckOffset = EnemyGroundInfo.jumpCheckOffset;
        mySpeed = EnemyGroundInfo.mySpeed;

        //Sets Attack Info
        attackRange = EnemyGroundInfo.attackRange;
        delayAfterAttack = EnemyGroundInfo.delayAfterAttack;
        delayAfterGettingHit = EnemyGroundInfo.delayAfterGettingHit;
        safetyRange = EnemyGroundInfo.safetyRange;
        isRanger = EnemyGroundInfo.isRanger;

        //Sets Custom Behavior
        isTurret = EnemyGroundInfo.isTurret;
        followEnabled = EnemyGroundInfo.followEnabled;
        jumpEnabled = EnemyGroundInfo.jumpEnabled;
        minJumpDistanceEnable = EnemyGroundInfo.minJumpDistanceEnabled;
        directionLookEnabled = EnemyGroundInfo.directionLookEnabled;
        
        //other
        deathParticles = EnemyGroundInfo.deathParticles;

        if (followEnabled)
        {
            movement = gameObject.GetComponent<PathFindingGround>();
            rangeAttack = gameObject.GetComponent<RangeAttack>();

            GameObject targetBody = GameObject.FindGameObjectWithTag(tagTarget);

            if (targetBody)
            {
                Seeker seeker = gameObject.GetComponent<Seeker>();
                Rigidbody2D rigidbody2D = seeker.gameObject.GetComponent<Rigidbody2D>();
                Transform target = targetBody.transform;

                if (movement)
                {
                    movement.SetPathfindingInfo(target, seeker, activateDistance, pathUpdateSeconds);
                    movement.SetPhsicsInfo(rigidbody2D, nextWaypointDistance, jumpNodeHeightRequirement,
                                           minPlayerHeightDistance, jumpModifier, jumpCheckOffset, mySpeed);
                    movement.SetCustomBehaviorInfo(followEnabled, jumpEnabled, minJumpDistanceEnable, directionLookEnabled);
                }
            }


        }
    }
}
