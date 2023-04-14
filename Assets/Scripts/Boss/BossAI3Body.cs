using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3Body : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 size;
    private RectTransform rectTransform;
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
        if (hitWall())
        {
            BossAI3.Instance.velocity = -BossAI3.Instance.velocity;
            BossAI3.Instance.BossMove();
        }

        if (start) return;

        if (hitGround())
        {
            BossAI3.Instance.rbZero();
        }

        if (hitPlayer())
        {
            start = true;
            BossAI3.Instance.BossHandL.SetActive(true);
            BossAI3.Instance.BossHandR.SetActive(true);
            BossAI3.Instance.phase = BossAI3.Phases.Phase1;
            BossAI3.Instance.startPhase();
        }
    }

    private bool hitWall()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(width, width), 0.0f, wallLayer);
    }

    private bool hitPlayer()
    {
        return Physics2D.OverlapBox(transform.position, size, 0.0f, playerLayer);
    }

    private bool hitGround()
    {
        return Physics2D.OverlapBox(transform.position, new Vector2(width/2, height), 0.0f, groundLayer);
    }

    // Check the size of OverlapBox
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, size);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawCube(transform.position, new Vector2(width,width));
    //}
}
