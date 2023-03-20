using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossAI2Buffalo : MonoBehaviour
{
    public float speed = 2;
    bool startmove = false;

    // Start is called before the first frame update
    private void OnEnable()
    {
        InitialMove();
    }

    private void Update()
    {
        if (startmove)
        {
            move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            startmove = true;
        }
        else if (collision.collider.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            BossAI2.Instance.motion[2] = false;
            BossAI2.Instance.motion[0] = true;
            BossAI2Snake.motionIndex = 3;
            BossAI2.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.boundary)
        {
            gameObject.SetActive(false);
            BossAI2.Instance.motion[2] = false;
            BossAI2.Instance.motion[0] = true;
            BossAI2Snake.motionIndex = 3;
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
        Vector2 dirVec = new Vector2(-1, 0);
        rigid.velocity = dirVec.normalized * speed;
    }
}
