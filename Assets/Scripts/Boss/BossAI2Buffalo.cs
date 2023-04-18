using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossAI2Buffalo : MonoBehaviour
{
    public float speed = 2;
    bool startmove;
    Vector2 InitialPos;

    private void Start()
    {
        InitialPos = this.transform.position;
        startmove = false;
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        InitialMove();
    }

    private void Initailization()
    {
        startmove = false;
        this.transform.position = InitialPos;
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Tilemap")
        {
            if (!startmove)
            {
                move();
                startmove = true;
            }
        }

        if (collision.CompareTag("Player"))
        {
            Initailization();
            gameObject.SetActive(false);
            BossAI2Snake.motionIndex = 2;
            BossAI2.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
            BossAI2.Instance.phase = BossAI2.Phases.snake;
            BossAI2.Instance.startPhase();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.boundary)
        {
            Initailization();
            gameObject.SetActive(false);
            BossAI2Snake.motionIndex = 2;
            BossAI2.Instance.phase = BossAI2.Phases.snake;
            BossAI2.Instance.startPhase();
        }
    }

    private void InitialMove()
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(1, Mathf.Sin(Mathf.PI * 1));
        rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
    }

    private void move()
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(1, 0);
        rigid.velocity = dirVec.normalized * speed;
    }
}
