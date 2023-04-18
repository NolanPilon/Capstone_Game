using UnityEngine;

public class BossAI2Bullet : MonoBehaviour
{
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;

    private void Update()
    {
        if (hitSomething()) 
        {
            Destroy(gameObject, 0.1f);
        }   
    }

    private bool hitSomething() 
    {
        return Physics2D.OverlapCircle(colliderPos.position, 0.2f, colliderLayer);
    }
}
