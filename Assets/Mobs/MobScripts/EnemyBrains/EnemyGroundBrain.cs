using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyGroundBrain : MonoBehaviour
{
    public EnemyGroundInfo EnemyGroundInfo;

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
    private float delayAfterAttack;
    private float delayAfterGettingHit;

    [Header("Custom Behavior")]
    private bool followEnabled;
    private bool jumpEnabled;
    private bool minJumpDistanceEnable;
    private bool directionLookEnabled ;

    private Path path;
    private int currentWaypoint;
    private bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
    void Start()
    {
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

        //Sets Custom Behavior
        followEnabled = EnemyGroundInfo.followEnabled;
        jumpEnabled = EnemyGroundInfo.jumpEnabled;
        minJumpDistanceEnable = EnemyGroundInfo.minJumpDistanceEnabled;
        directionLookEnabled = EnemyGroundInfo.directionLookEnabled;

        if (followEnabled)
        {
            GameObject targetBody = GameObject.FindGameObjectWithTag(tagTarget);
            if (targetBody)
            {
                var movement = gameObject.GetComponent<PathFindingGround>();
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