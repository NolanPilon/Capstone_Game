using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed = 0.5f;
    private Rigidbody2D rb;
    public Transform target;
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = (direction * projectileSpeed);
    }

    private void Update()
    {
        if (hitSomething()) 
        {
            transform.position = new Vector2(1000, 1000);
            Destroy(gameObject, 0.1f);
        }   
    }

    private bool hitSomething() 
    {
        return Physics2D.OverlapCircle(colliderPos.position, 0.2f, colliderLayer);
    }
}
