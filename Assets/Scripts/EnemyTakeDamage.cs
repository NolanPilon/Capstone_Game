using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    //Required speed to be damaged
    private int requiredSpeed = 28;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.playerSpeed > requiredSpeed)
        {
            StartCoroutine(AttackFreeze());
            transform.position = new Vector2(1000, 1000);
            Destroy(this.gameObject, 0.2f);
        }
    }

    IEnumerator AttackFreeze() 
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1.0f;
    }
}
