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
    public GameObject spawnPoint;

    private float moveSpeed = 10;
    private float horizontalMove;
    private int jumpForce = 19;

    //Coyote time
    private float coyoteBuffer = 0.2f;
    private float coyoteBufferTimer;
    private bool playedLanding = false;

    //Jump buffering
    private float jumpBuffer = 0.2f;
    private float jumpBufferTimer;


    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    private Rigidbody2D playerRB;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();

        if (GameManager.respawnPoint != Vector2.zero && GameManager.progressPoint != 0)
        {
            this.transform.position = GameManager.respawnPoint;
        }
        else if (GameManager.progressPoint == 0) 
        {
            transform.position = spawnPoint.transform.position;
        }
    } 

    void Update()
    {
        // disable controlls during certain events
        if (controlsEnabled) 
        {
            horizontalMove = Input.GetAxisRaw("Horizontal");

            PlayerMovement(horizontalMove);

            // Coyote time timer
            if (isGrounded())
            {
                coyoteBufferTimer = coyoteBuffer;

                // Reset parry combo
                GameManager.parryCombo = 0;
            }
            else
            {
                coyoteBufferTimer -= Time.deltaTime;
            }

            if (jumpBufferTimer > 0 && coyoteBufferTimer > 0)
            {
                jumpBufferTimer = 0;
                Jump();
            }

            // Prevents double jump and allows for variable jump height
            if (Input.GetKeyUp(KeyCode.Space) && playerRB.velocity.y > 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);

                coyoteBufferTimer = 0;
            }

            // Flip sprite
            if (playerRB.velocity.x < 0)
            {
                if (GetComponent<SpriteRenderer>().flipX != true && playerRB.velocity.x < 10) 
                {
                    CreateDust();
                }   
                GetComponent<SpriteRenderer>().flipX = true;
              
            }
            else 
            {
                if (GetComponent<SpriteRenderer>().flipX != false && playerRB.velocity.x > -10) 
                {
                    CreateDust();
                }
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
        // Buffer player jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferTimer = jumpBuffer;
        }
        else if (jumpBufferTimer > -0.5)
        {
            jumpBufferTimer -= Time.deltaTime;
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
        return Physics2D.OverlapCircle(groundCheck.position, 0.4f, groundLayer);
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
        Gizmos.DrawSphere(groundCheck.position, 0.4f);
    }
}


