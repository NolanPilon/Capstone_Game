using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCombatFunctions : MonoBehaviour
{
    public int health;
    private float invincibilityFrames = 1.5f;
    private float invincibilityTimer;
    private Rigidbody2D playerRB;
    private Vector2 vel;
    private SpriteRenderer playerSprite;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerRB = GetComponent<Rigidbody2D>();
        health = 3;
    }
    void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        else 
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
        }

        vel = playerRB.velocity;
        GameManager.playerSpeed = vel.magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.playerSpeed < 28 && collision.tag == "Enemy") 
        {
            if (invincibilityTimer <= 0)
            {
                takeDamage();
            }
        }

        if (collision.tag == "Bullet" && !ParryBehavior.inParry) 
        {
            takeDamage(); 
        } 
    }

    public void takeDamage() 
    {
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.4f);
        invincibilityTimer = invincibilityFrames;

        health--;
    }
}
