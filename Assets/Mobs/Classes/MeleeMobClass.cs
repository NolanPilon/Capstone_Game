using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MeleeMobClass : BaseMobClass
{

    public Transform target;
    public float nextWayPointDistance = 3f;
    public float MySpeed = 200f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    //Mob will move Towards the player to get in range
    void moveTowardsPlayer()
    {
        
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint= 0;
        }
    }
}
