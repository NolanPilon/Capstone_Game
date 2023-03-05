/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCombatFunctions : MonoBehaviour
{
    private int knockBackForce = 25;
    private float invincibilityFrames = 1.5f;
    private float invincibilityTimer;

    private Rigidbody2D playerRB;
    //private Vector2 vel;
    private SpriteRenderer playerSprite;
   
    public PlayerControlls playerController;

    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        playerRB = GetComponent<Rigidbody2D>();
        GameManager.playerHP = 3;
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
        //vel = playerRB.velocity;
        //GameManager.playerSpeed = vel.magnitude;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.parryCombo < 2 && collision.tag == "Enemy") 
        {
            if (invincibilityTimer <= 0)
            {
                takeDamage(collision.gameObject.transform.position);
            }
        }

        if (collision.tag == "Bullet" && !ParryBehavior.inParry) 
        {
            if (invincibilityTimer <= 0)
            {
                takeDamage(collision.gameObject.transform.position);
            }
        } 
    }

    public void takeDamage(Vector3 enemyPos) 
    {
        playerController.controlsEnabled = false;
        CameraShake.Instance.OnShakeCameraPosition(0.2f, 0.1f);
        Vector3 knockbackDir = (transform.position - enemyPos).normalized;
        playerRB.AddForce(knockbackDir * knockBackForce, ForceMode2D.Impulse);
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.4f);
        invincibilityTimer = invincibilityFrames;

        GameManager.playerHP--;
    }

    public void launchOpposite() 
    {
        playerRB.velocity = -playerRB.velocity;
    }
}

 */
