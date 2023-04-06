using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnSnow : MonoBehaviour
{
    public ParticleSystem snowParticles;
    [SerializeField] private GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Boss")
        {
            if (!boss.GetComponent<BossAI>().getIfMotion2())
            {
                SoundManager.Instance.PlayBossAt2();
                snowParticles.Play();
            }
        }
        
    }


}
