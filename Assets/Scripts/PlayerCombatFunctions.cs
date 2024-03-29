using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerCombatFunctions : MonoBehaviour
{
    public PlayerControlls playerController;
    private int knockBackForce = 25;
    private float invincibilityFrames = 1.5f;
    private float invincibilityTimer;

    private Rigidbody2D playerRB;
    private SpriteRenderer playerSprite;

    [SerializeField] private Collider2D combatCollider;

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
            combatCollider.enabled = false; 
        }
        else 
        {
            playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            combatCollider.enabled = true;
        }
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

        if (collision.tag == "Obstacle")
        {
            if (invincibilityTimer <= 0)
            {
                takeDamage(collision.gameObject.transform.position);
            }
        }

        if (collision.tag == "ParryBullet" && !ParryBehavior.inParry && GameManager.parryCombo <= 0)
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
        DamageVignette.vignetteValue = 0.35f;

        GameManager.playerHP--;
        SoundManager.Instance.PlayHurt();
    }

    public void launchOpposite() 
    {
        playerRB.velocity = -playerRB.velocity;
    }
}
