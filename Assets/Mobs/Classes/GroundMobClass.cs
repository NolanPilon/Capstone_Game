using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GroundMobClass : BaseMobClass
{
    //Later on will change stuff into programmable  objects to avoid stuff like (float speed in both classes)
    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50.0f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float nextWaypointDistance = 3.0f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float minPlayerHeightDistance = 10;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;
    public float mySpeed = 200f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool minJumpDistanceEnabled = false;
    public bool directionLookEnabled = true;

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

    private void FixedUpdate()
    {
        if(TargetInDistance() && followEnabled)
        {
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if(followEnabled && TargetInDistance() && seeker.IsDone())
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

        //See if colliding with anything
        Vector3 startOffset = transform.position - new Vector3(0.0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        // creates the vector towards the player
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * mySpeed * Time.deltaTime;

        float jumpHeightDistance = player.transform.position.y - this.transform.position.y;

        //Jump
        if (jumpEnabled && isGrounded)
        {

            if(direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * mySpeed * jumpModifier);
            }
            else if(minJumpDistanceEnabled)
            {
                if(jumpHeightDistance >= minPlayerHeightDistance)
                {
                    rb.AddForce(Vector2.up * mySpeed * jumpModifier);
                }
            }
        }

        //Moves the mob
        rb.AddForce(force);

        //Gets the next waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Flip the GFX
        if(directionLookEnabled)
        {
            if(rb.velocity.x > 0.05f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if(rb.velocity.x < -0.05f)
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
