using UnityEngine;

public class BossAI2Parry : MonoBehaviour
{
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;

    private void Update()
    {
        if (hitSomething() || BossAI2.Instance.phase != BossAI2.Phases.snake) 
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
