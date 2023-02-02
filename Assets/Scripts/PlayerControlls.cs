using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControlls : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10;
    private int jumpForce = 12;
    public bool canJump = false;

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
            canJump= true;
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            canJump = false;
        }
    }

    void PlayerMovement()
    {
        transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);
    }

    void Jump() 
    {
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jumped");
        }
    }
}


