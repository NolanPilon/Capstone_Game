using UnityEngine;

public class BossAI3ParryObject : MonoBehaviour
{
    private void Update()
    {
        if (BossAI3.Instance.phase != BossAI3.Phases.AttackPhase)
        {
            transform.position = new Vector2(1000, 1000);
            Destroy(gameObject, 0.1f);
        }
    }
}
