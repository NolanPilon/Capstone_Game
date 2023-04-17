using UnityEngine;

public class BossAI3Eye : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (BossAI3.Instance.phase == BossAI3.Phases.AttackPhase)
            {
                if (GameManager.parryCombo >= 2)
                {
                    BossAI3.Instance.BossHealth--;
                    BossAI3.Instance.LightOfSight.SetActive(false);
                    CameraShake.Instance.OnShakeCameraPosition(0.4f, 0.2f);

                    BossAI3.Instance.target.GetComponent<PlayerControlls>().controlsEnabled = false;
                    Vector3 knockbackDir = (transform.position).normalized;
                    BossAI3.Instance.target.GetComponent<Rigidbody2D>().AddForce(knockbackDir * 25, ForceMode2D.Impulse);
                    
                    SoundManager.Instance.PlayBossHit();

                    if (BossAI3.Instance.BossHealth == 2)
                    {
                        BossAI3.Instance.phase = BossAI3.Phases.Phase2;
                        BossAI3.Instance.startPhase();
                    }
                    else if (BossAI3.Instance.BossHealth == 1)
                    {
                        BossAI3.Instance.phase = BossAI3.Phases.Phase3;
                        BossAI3.Instance.startPhase();
                    }
                    else
                    {
                        CameraShake.Instance.OnShakeCameraPosition(2.0f, 0.1f);
                        BossAI3.Instance.phase = BossAI3.Phases.Die;
                        BossAI3.Instance.startPhase();
                    }
                }
                else
                {
                    BossAI3.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
                }
            }
            else
            {
                CameraShake.Instance.OnShakeCameraPosition(0.2f, 0.1f);

                BossAI3.Instance.target.GetComponent<PlayerControlls>().controlsEnabled = false;
                Vector3 knockbackDir = (transform.position).normalized;
                BossAI3.Instance.target.GetComponent<Rigidbody2D>().AddForce(knockbackDir * 25, ForceMode2D.Impulse);

                BossAI3.Instance.phase = BossAI3.Phases.AttackPhase;
                BossAI3.Instance.startPhase();

                SoundManager.Instance.PlayBossHit();
            }
        }
    }
}
