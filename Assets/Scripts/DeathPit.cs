using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPit : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.transform.position = GameManager.respawnPoint;
            GameManager.playerHP -= 1;
            CameraShake.Instance.OnShakeCameraPosition(0.2f, 0.2f);
        }
        else if (collision.tag == "Enemy") 
        {
            Destroy(collision.gameObject);
        }
    }

}
