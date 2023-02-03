using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AirMobClass : BaseMobClass
{
    public GameObject projectile;
    int range;


    public Transform target;
    public float nextWayPointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveTowardsPlayer();
    }

    //
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    //Mob will move Towards the player to get in range
    void moveTowardsPlayer()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        // creates the vector towards the player
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance)
        {
            currentWaypoint++;
        }

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

    //checks if the player is in shooting range
    void isInRange()
    {

    }

    //move the player in attack range
    void getInRange()
    {

    }

    //mob repositions to a safe location
    void runAway()
    {
        
    }

    //checks if the mob is too close to the player (danger)
    bool isSafe()
    {
        //run away
        return false;
    }

}
