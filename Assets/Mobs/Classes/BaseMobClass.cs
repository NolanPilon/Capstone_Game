using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseMobClass : MonoBehaviour
{

    //THE PLAYER
    public GameObject player;

    //Mob stats
    private int health = 999;
    private float speed;
    private int damage;
    private bool dead = false;

    public float maxRange = 0.8f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns the  health
    public int GetHealth()
    {
        return health;
    }

    //Intakes damage done to the mob
    void TakeDamage(int dmgTaken)
    {
        //should probably intake damage from play - Heatlh
        health -= dmgTaken;
    }

    public float GetSpeed()
    {
        //should probably intake damage from play - Heatlh
        return speed;
    }

    //Returns the Mobs Damage 
    public int GetDamage()
    {
        return damage;
    }

    //allows the mob to attack
    void Attack()
    {
        //will use the damage value
        //Different mobs will have different attack info
        //Add delay to the attack so that the mob can 1shot player   
    }


    //checks if the player is within range of the mob(range of attack)
    bool isInRange(float maxRange)
    {
        Vector2 distance = player.transform.position - this.transform.position;

        if (distance.magnitude <= maxRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //will kill the mob
    void Die()
    {  
        //play death animation

        Destroy(this.gameObject);

        //add score
    }
}
