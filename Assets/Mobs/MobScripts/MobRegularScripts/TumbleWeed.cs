using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TumbleWeed : MonoBehaviour
{

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask groundLayer = 0;

    private Rigidbody2D rb;
    private int jumpForce;

    public int maxJumpForce;
    public int minJumpForce;
    public int jumpConsistentsy;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpForce = Random.Range(minJumpForce, maxJumpForce);

        if(checkIfGround() && Random.Range(1,100) > jumpConsistentsy)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }

    public bool checkIfGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);

    }
}
