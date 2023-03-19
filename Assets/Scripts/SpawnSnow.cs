using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnSnow : MonoBehaviour
{
    public ParticleSystem snowParticles;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameObject.Find("Boss").GetComponent<BossAI>().getIfMotion2())
        {
            snowParticles.Play();
        }
    }


}
