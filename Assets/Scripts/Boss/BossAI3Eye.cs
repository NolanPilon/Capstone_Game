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
            BossAI3.Instance.BossHealth--;
            BossAI3.Instance.target.transform.position = BossAI3.Instance.targetSpawner.position;
        }
    }
}
