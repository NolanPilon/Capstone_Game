using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerStats : MonoBehaviour
{
    private int health;
    private float currentSpeed;
    private float invincibilityFrames;
    private Rigidbody2D playerRB;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        health = 3;
    }
    void Update()
    {
        currentSpeed = Mathf.Abs(playerRB.velocity.x + playerRB.velocity.y);
        GameManager.playerSpeed = currentSpeed;
    }

    public void takeDamage(int damage) 
    {
        health -= damage;
    }
}
