using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TumbleWeed : MonoBehaviour
{

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask groundLayer = 0;
    [SerializeField] private int maxJumpForce;
    [SerializeField] private int minJumpForce;
    [SerializeField] private int jumpConsistentsy;

    private Rigidbody2D rb;
    private int jumpForce;
    private bool canJump = true;
  
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        OnDrawGizmos();
    }

    // Update is called once per frame
    void Update()
    {
    
        jumpForce = Random.Range(minJumpForce, maxJumpForce);

        if (checkIfGround() && Random.Range(1,100) > jumpConsistentsy)
        {
            if(canJump)
            {
                rb.AddForce(Vector2.up * jumpForce);
                canJump = false;
                StartCoroutine(delayJump());
            }

        }
    }

    public bool checkIfGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.5f, groundLayer);

    }

    IEnumerator delayJump()
    {
        yield return new WaitForSeconds(2);

        canJump = true;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, 0.4f);
    }

}
