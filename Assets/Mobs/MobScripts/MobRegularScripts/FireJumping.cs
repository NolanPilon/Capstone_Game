using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireJumping : MonoBehaviour
{

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask groundLayer = 0;

    private Rigidbody2D rb;
    private int jumpForce = 30;
    private bool canJump = true;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        OnDrawGizmos();
    }

    // Update is called once per frame
    void Update()
    {

        if (checkIfGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

             rb.AddForce(Vector2.up * jumpForce);
            //canJump = false;
        }
    }

    public bool checkIfGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);

    }

    IEnumerator delayJump()
    {
        yield return new WaitForSeconds(1);

        canJump = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.4f);
    }

}
