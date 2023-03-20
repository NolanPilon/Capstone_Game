using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI2Parry : MonoBehaviour
{
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;
    [SerializeField] private LayerMask playerLayer;

    private void Update()
    {
        if (hitSomething() || !BossAI2.Instance.motion[0]) 
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
