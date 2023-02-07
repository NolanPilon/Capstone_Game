using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFindingGround : MonoBehaviour
{
    //Later on will change stuff into programmable  objects to avoid stuff like (float speed in both classes)
    [Header("Pathfinding")]
    private Transform target;
    private float activateDistance = 50.0f;
    private float pathUpdateSeconds = 0.5f;
    private bool isGroundOrAir;

    [Header("Physics")]
    private float nextWaypointDistance = 3.0f;
    private float jumpNodeHeightRequirement = 0.8f;
    private float minPlayerHeightDistance = 10;
    private float jumpModifier = 0.3f;
    private float jumpCheckOffset = 0.1f;
    private float mySpeed = 200f;

    [Header("Attack Info")]
    private float attackRange = 0.8f;
    private float delayAfterAttack = 3.0f;
    private float delayAfterGettingHit = 3.0f;

    [Header("Custom Behavior")]
    private bool followEnabled = true;
    private bool jumpEnabled = true;
    private bool minJumpDistanceEnabled = false;
    private bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    private bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;

    public void Start()
    {
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }
    public void SetPathfindingInfo(Transform useThisTarget, Seeker useThisSeeker, float actiDistamce,float pathUpdateSecs )
    {
        target = useThisTarget;
        activateDistance = actiDistamce;
        pathUpdateSeconds = pathUpdateSecs;
        seeker = useThisSeeker;
    }

    public void SetPhsicsInfo(Rigidbody2D useThisRb,float nextWayPointDis,float jumpRequirement, float minPlayerheight, float jumpMod, float jumpCheckOff,float speed)
    {
        rb = useThisRb;
        nextWaypointDistance = nextWayPointDis;
        jumpNodeHeightRequirement = jumpRequirement;
        minPlayerHeightDistance = minPlayerheight;
        jumpModifier = jumpMod;
        jumpCheckOffset = jumpCheckOff;
        mySpeed = speed;
    }

    public void SetCustomBehaviorInfo(bool isFollow, bool isJump, bool minJumpDistanceEnable, bool isdirectionLook)
    {
        followEnabled = isFollow;
        jumpEnabled = isJump;
        minJumpDistanceEnabled = minJumpDistanceEnable;
        directionLookEnabled = isdirectionLook;
    }

    void FixedUpdate()
    {

       // // go towards the player
       // if (TargetInDistance() && followEnabled)
       // {
       //     PathFollow();
       // }
    }  

    protected void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    public void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        //Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        //See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0.0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // creates the vector towards the player
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * mySpeed * Time.deltaTime;

        float jumpHeightDistance = target.transform.position.y - this.transform.position.y;

        //Jump
        if (jumpEnabled && isGrounded)
        {

            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * mySpeed * jumpModifier);
            }
            else if (minJumpDistanceEnabled)
            {
                if (jumpHeightDistance >= minPlayerHeightDistance)
                {
                    rb.AddForce(Vector2.up * mySpeed * jumpModifier);
                }
            }
        }

        //Moves the mob
        rb.AddForce(force);

        //Gets the next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Flip the GFX
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    //check if player is in mob range
    public bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    public float TargetDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position);
    }

    //activates when mob reaches the end of path
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
