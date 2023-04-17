using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public int NumProgressPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.progressPoint = NumProgressPoint;
            GameManager.currentCheckpoint = this.gameObject.transform.position;
            GameManager.respawnPoint = this.gameObject.transform.position;
            GameManager.progressPoint = NumProgressPoint;
        }
    }
}
