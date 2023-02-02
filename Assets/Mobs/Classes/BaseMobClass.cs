using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseMobClass : MonoBehaviour
{

    //THE PLAYER
    public GameObject player;
    //Mob stats
    private int Health = 66;
    private float Speed;
    private int damage;
    private bool dead = false;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Returns the  health
    public int getHealth()
    {
        return Health;
    }

    //Intakes damage done to the mob
    void takeDamage(int dmgTaken)
    {
        //should probably intake damage from play - Heatlh
        Health -= dmgTaken;
    }

    public float getSpeed()
    {
        //should probably intake damage from play - Heatlh
        return Speed;
    }

    //Returns the Mobs Damage 
    public int getDamage()
    {
        return damage;
    }

    //allows the mob to attack
    void attack()
    {
        //will use the damage value
        //Different mobs will have different attack info
        //Add delay to the attack so that the mob can 1shot player   
    }

    //will kill the mob
    void die()
    {  
        //play death animation

        Destroy(this.gameObject);

        //add score
    }
}
