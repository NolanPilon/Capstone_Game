using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UIElements;

public class BossAI2Wolf : MonoBehaviour
{
    private float speed;

    // Start is called before the first frame update
    private void OnEnable()
    {
        InitialMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            BossAI2.Instance.motion[1] = false;
            BossAI2.Instance.motion[0] = true;
            BossAI2Snake.motionIndex = 2;
            BossAI2.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
        }
        else if (collision.collider.CompareTag("Wall"))
        {
            move();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.boundary)
        {
            gameObject.SetActive(false);
            BossAI2.Instance.motion[1] = false;
            BossAI2.Instance.motion[0] = true;
            BossAI2Snake.motionIndex = 2;
        }
    }

    private void InitialMove()
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(-1, Mathf.Sin(Mathf.PI * 1));
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
    }

    private void move()
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(-1, 1);
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
    }
}
