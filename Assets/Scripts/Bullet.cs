using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float projectileSpeed = 0.5f;
    private Rigidbody2D rb;
    public Transform target;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Player").transform;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = (direction * projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            //dmg player
            Destroy(gameObject);
        }
        if (collision.gameObject.layer == 3 || collision.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}
