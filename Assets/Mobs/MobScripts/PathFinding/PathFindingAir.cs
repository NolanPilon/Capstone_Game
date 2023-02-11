using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathFindingAir : MonoBehaviour
{
    [Header("Pathfinding")]
    private Transform target;
    private float activateDistance = 50.0f;
    private float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    private float nextWaypointDistance = 3.0f;
    private float mySpeed = 200f;

    [Header("Custom Behavior")]
    private bool followEnabled = true;
    private bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;

    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    public void SetPathfindingInfo(Transform useThisTarget, Seeker useThisSeeker, float actiDistamce, float pathUpdateSecs)
    {
        target = useThisTarget;
        activateDistance = actiDistamce;
        pathUpdateSeconds = pathUpdateSecs;
        seeker = useThisSeeker;
    }
    
    public void SetPhysicsInfo(float nextWaypointDis, float speed)
    {
        mySpeed = speed;
        nextWaypointDistance = nextWaypointDis;
    }

    public void SetCustomBehavior(bool followEnable,bool directionLook)
    {
        followEnabled = followEnable;
        directionLookEnabled = directionLook;
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
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

        // creates the vector towards the player
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * mySpeed * Time.deltaTime;


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

    //aggro distance
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
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