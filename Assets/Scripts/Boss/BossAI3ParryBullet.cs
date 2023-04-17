using UnityEngine;

public class BossAI3ParryBullet : MonoBehaviour
{
    [SerializeField] private Transform colliderPos;
    [SerializeField] private LayerMask colliderLayer;

    private void Update()
    {
        if (hitSomething() || BossAI3.Instance.phase == BossAI3.Phases.AttackPhase)
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
