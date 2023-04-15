using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BossAI3Eye : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (BossAI3.Instance.phase == BossAI3.Phases.AttackPhase)
            {
                BossAI3.Instance.BossHealth--;
                BossAI3.Instance.target.transform.position = BossAI3.Instance.targetSpawner.position;

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
            }
            else
            {
                BossAI3.Instance.phase = BossAI3.Phases.AttackPhase;
                BossAI3.Instance.startPhase();
            }
        }
    }
}
