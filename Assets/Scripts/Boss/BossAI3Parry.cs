using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3Parry : MonoBehaviour
{
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;

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
