using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlls : MonoBehaviour
{
    public bool controlsEnabled = true;

    public ParticleSystem dust;
    public ParticleSystem landingDust;

    private float moveSpeed = 10;
    private float horizontalMove;
    private int jumpForce = 19;

    //Coyote time
    private float jumpBuffer = 0.2f;
    private float jumpBufferTimer;
    private bool playedLanding = false;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    private Rigidbody2D playerRB;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();

        if (GameManager.respawnPoint != Vector2.zero )
        {
            this.transform.position = GameManager.respawnPoint;
        }
    } 

    void Update()
    {
        // disable controlls during certain events
        if (controlsEnabled) 
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");

            PlayerMovement(horizontalMove);

            // Jump buffer timer
            if (isGrounded())
            {
                jumpBufferTimer = jumpBuffer;

                // Reset parry combo
                GameManager.parryCombo = 0;
            }
            else
            {
                jumpBufferTimer -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) && jumpBufferTimer > 0)
            {
                Jump();
            }

            // Prevents double jump and allows for variable jump height
            if (Input.GetKeyUp(KeyCode.Space) && playerRB.velocity.y > 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);

                jumpBufferTimer = 0;
            }

            // Flip sprite
            if (playerRB.velocity.x < 0)
            {
                if (GetComponent<SpriteRenderer>().flipX != true && playerRB.velocity.x < 10)
                    CreateDust();
                GetComponent<SpriteRenderer>().flipX = true;
              
            }
            else 
            {
                if (GetComponent<SpriteRenderer>().flipX != false && playerRB.velocity.x > -10)
                    CreateDust();
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (playerRB.velocity.y < -20)
            {
                playedLanding = false;
            }
            if (isGrounded() && playedLanding == false)
            {
                CreateLandingDust();
                playedLanding = true;
            }
            
        }
    }

    //Using velocity based movement to prevent jittering and clipping
    private void PlayerMovement(float xMove)
    { 
        playerRB.velocity = new Vector2((horizontalMove * moveSpeed), playerRB.velocity.y);
    }

    private void Jump()
    {
        CreateDust();
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        SoundManager.Instance.PlayJump();
    }

    //Draws an invisible circle to check if on the ground
    public bool isGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);
    }

    void CreateDust()
    {
        if(isGrounded())
        dust.Play();
    }

    void CreateLandingDust()
    {
        landingDust.Play();
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.5f);
    }
}


