using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControlls : MonoBehaviour
{
    public bool controlsEnabled = true;

    private float moveSpeed = 10;
    private float horizontalMove;
    private int jumpForce = 18;

    //Coyote time
    private float jumpBuffer = 0.2f;
    private float jumpBufferTimer;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    

    private Rigidbody2D playerRB;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
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
        }
    }

    //Using velocity based movement to prevent jittering and clipping
    private void PlayerMovement(float xMove)
    { 
        playerRB.velocity = new Vector2((horizontalMove * moveSpeed), playerRB.velocity.y);
    }

    private void Jump() 
    {   
        playerRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //Draws an invisible circle to check if on the ground
    public bool isGrounded() 
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}


