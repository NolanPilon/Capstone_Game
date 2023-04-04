using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3Body : MonoBehaviour
{
    public GameObject Boss3;
    [SerializeField] private LayerMask colliderLayer;
    private Vector2 size;
    RectTransform rectTransform;
    private float height;
    private float width;
    private bool start = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        height = rectTransform.rect.height * rectTransform.localScale.y;
        width = rectTransform.rect.width * rectTransform.localScale.x;
        size = new Vector2(width, height);
    }

    private void Update()
    {
        if (start) return;

        if (hitSomething())
        {
            BossAI3.Instance.BossHandR.SetActive(true);
            BossAI3.Instance.BossHandL.SetActive(true);
            start = true;
        }
    }

    private bool hitSomething()
    {
        return Physics2D.OverlapBox(transform.position, size, 0.0f, colliderLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Rigidbody2D rd = Boss3.GetComponent<Rigidbody2D>();
            rd.gravityScale = 0;
            rd.velocity = Vector2.zero;
        }
    }

    // Check the size of OverlapBox
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, size);
    //}
}
