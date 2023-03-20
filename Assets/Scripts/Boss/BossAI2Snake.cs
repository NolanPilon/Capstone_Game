using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossAI2Snake : MonoBehaviour
{
    public static int motionIndex;
    private Vector2 InitialPos;
    public Transform ParryPos;
    public float speed = 2.0f;

    public GameObject parry;
    private int curPatternCount;
    public int maxPatternCount;
    private bool shootStart;
    private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        InitialPos = transform.position;
        curPatternCount = 0;
        motionIndex = 0;
        shootStart= false;
    }

    private void Update()
    {
        if (motionIndex == 0) return;

        if (BossAI2.Instance.motion[0])
        {
            move();

            if (this.transform.position == ParryPos.position && !shootStart)
            {
                ParryShoot();
                shootStart = true;
            }
        }
        else
        {
            moveback();
            BossAI2.Instance.motion[motionIndex] = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.target)
        {
            if (GameManager.parryCombo >= 2)
            {
                BossAI2.Instance.BossHealth--;
                curPatternCount = 0;
                BossAI2.Instance.motion[0] = false;
                shootStart = false;
                Destroy(bullet);
            }
            else
            {
                BossAI2.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
            }
        }
    }

    private void move()
    {
        transform.position = Vector2.MoveTowards(transform.position, ParryPos.position, speed * Time.deltaTime);
    }

    private void moveback()
    {
        transform.position = Vector2.MoveTowards(transform.position, InitialPos, speed * Time.deltaTime);
    }

    private void ParryShoot()
    {
        int roundNumA = 20;

        for (int i = 0; i < roundNumA; i++)
        {
            bullet = Instantiate(parry, transform.position, transform.rotation);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA), Mathf.Sin(Mathf.PI * 2 * i / roundNumA));
            rigid.velocity = dirVec * 5;
        }

        curPatternCount++;

        if (curPatternCount < maxPatternCount)
        {
            Invoke("ParryShoot", 0.7f);
        }
    }
}
