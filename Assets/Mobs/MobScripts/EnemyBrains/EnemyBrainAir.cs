using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyBrainAir : MonoBehaviour
{

    public EnemyAirInfo EnemyAirInfo;

    [Header("EnemyStats")]
    private int health;
    private int damage;

    [Header("Pathfinding")]
    private string tagTarget;
    private float activateDistance;
    private float pathUpdateSeconds;

    [Header("Physics")]
    private float nextWaypointDistance;
    private float mySpeed;

    [Header("Attack Info")]
    private float attackRange;
    private float safetyRange;
    private float delayAfterAttack;
    private float delayAfterGettingHit;
    private bool isRanger;



    [Header("Custom Behavior")]
    private bool followEnabled = true;
    private bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    private float NextAttack;
    bool isGrounded = false;

    PathFindingAir movement;
    RangeAttack rangeAttack;
    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        startUP();
    }

    private void FixedUpdate()
    {
        //if alive 
        if (health > 0)
        {   //checks if target is in agro distance and if the mob can move also added a delay if it attacks
            if (movement.TargetInDistance() && followEnabled || Time.time > NextAttack)
            {
                // if in attack distance
                if (movement.TargetDistance() > attackRange)
                {
                    movement.PathFollow();
                }
                // keeps the mob safe 
                else if (movement.TargetDistance() < safetyRange)
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
            // mob is dead 
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Time.time > NextAttack)
        {
            NextAttack = Time.time + delayAfterAttack;
        }
    }


    private void startUP()
    {
        //Sets Mob Stats
        health = EnemyAirInfo.health;
        damage = EnemyAirInfo.damage;

        //Sets Pathfinding Info
        tagTarget = EnemyAirInfo.targetTag;
        activateDistance = EnemyAirInfo.activateDistance;
        pathUpdateSeconds = EnemyAirInfo.pathUpdateSeconds;


        //Sets Physics info
        nextWaypointDistance = EnemyAirInfo.nextWaypointDistance;
        mySpeed = EnemyAirInfo.mySpeed;

        //Sets Attack Info
        attackRange = EnemyAirInfo.attackRange;
        delayAfterAttack = EnemyAirInfo.delayAfterAttack;
        delayAfterGettingHit = EnemyAirInfo.delayAfterGettingHit;
        safetyRange = EnemyAirInfo.safetyRange;
        isRanger = EnemyAirInfo.isRanger;

        //Sets Custom Behavior
        followEnabled = EnemyAirInfo.followEnabled;
        directionLookEnabled = EnemyAirInfo.directionLookEnabled;

        if (followEnabled)
        {
            movement = gameObject.GetComponent<PathFindingAir>();
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
                    movement.SetPhysicsInfo(rigidbody2D, nextWaypointDistance, mySpeed);
                    movement.SetCustomBehavior(followEnabled, directionLookEnabled);
                }
            }
        }
    }
}

