using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMobClass : MonoBehaviour
{
    //Mob stats
    int Health;
    float Speed;
    int damage;
    bool dead = false;


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
    int takeDamage()
    {
        //should probably intake damage from play - Heatlh
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
        Destroy();
        //add score
    }
}
