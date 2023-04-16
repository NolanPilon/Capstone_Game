using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float projectileSpeed = 0.5f;
    public bool noAIm = false;
    public Transform target;

    private Rigidbody2D rb;
    [SerializeField] private Transform fireDirection;
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;
        if(!noAIm)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = (direction * projectileSpeed);
        }
        else
        {
            Vector2 direction = (fireDirection.position - transform.position).normalized;
            rb.velocity = (direction * projectileSpeed);
        }
   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.position = new Vector2(1000, 1000);
        Destroy(gameObject, 0.1f);
    }
}
