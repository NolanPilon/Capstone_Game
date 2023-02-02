using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    private int jumpForce = 12;
    private float jumpBuffer = 0.1f;
    private bool canJump = false;
    private bool grounded = false;
    private bool jumped = false;

    private Rigidbody2D rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerMovement();
        Jump();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground") 
        {
            jumpBuffer = 0.1f;
            jumped = false;
            grounded = true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            grounded = false;
        }
    }

    void PlayerMovement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
    }

    void Jump() 
    {
        if (grounded)
        {
            canJump = true;
        }
        else if (jumpBuffer > 0 && !grounded)
        {
            jumpBuffer -= Time.deltaTime;
        }

        if (jumpBuffer > 0 && !jumped) 
        {
            canJump = true;
        }

        if (canJump && jumped) 
        {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            canJump = false;
            jumped = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jumped");
        }
    }
}


