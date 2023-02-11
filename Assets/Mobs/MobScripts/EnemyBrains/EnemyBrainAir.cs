using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainAir : MonoBehaviour
{
    public EnemyAirInfo EnemyAirInfo;

    [Header("Pathfinding")]
    private string tagTarget;
    private float activateDistance = 50.0f;
    private float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    private float nextWaypointDistance = 3.0f;
    private float mySpeed = 200f;

    [Header("Attack Info")]
    private float attackRange;
    private float delayAfterAttack;
    private float delayAfterGettingHit;

    [Header("Custom Behavior")]
    private bool followEnabled = true;
    private bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
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

        //Sets Custom Behavior
        followEnabled = EnemyAirInfo.followEnabled;
        directionLookEnabled = EnemyAirInfo.directionLookEnabled;

        if(followEnabled)
        {
            GameObject targetBody = GameObject.FindGameObjectWithTag(tagTarget);
            if (targetBody)
            {
                var movement = gameObject.GetComponent<PathFindingAir>();
                Seeker seeker = gameObject.GetComponent<Seeker>();
                Rigidbody2D rigidbody2D = seeker.gameObject.GetComponent<Rigidbody2D>();
                Transform target = targetBody.transform;

                if (movement)
                {
                    movement.SetPathfindingInfo(target, seeker, activateDistance, pathUpdateSeconds);
                    movement.SetPhysicsInfo(nextWaypointDistance, mySpeed);
                    movement.SetCustomBehavior(followEnabled, directionLookEnabled);
                }
            }
        }
    }
}

