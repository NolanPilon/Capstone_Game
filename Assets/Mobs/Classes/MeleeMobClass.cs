using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMobClass : BaseMobClass
{
    // Start is called before the first frame update
    public float maxRange = 0.8f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    
    //checks if the player is within range of the mob
   bool isInMeleeRange(float maxRange)
    {
        Vector2 distance = player.transform.position - this.transform.position;

        if(distance.magnitude <= maxRange)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }

    //Mob will move Towards the player to get in range
    void moveTowardsPlayer()
    {
        
    }
}
