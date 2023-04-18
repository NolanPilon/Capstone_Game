using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.GraphicsBuffer;

public class BossAI2Snake : MonoBehaviour
{
    public static int motionIndex;
    private Vector3 InitialPos;
    private Vector3 snakeHeadPos;
    public Transform ParryPos;
    public Transform snakeSitPos;
    public float speed = 2.0f;

    public GameObject parry;
    public GameObject snakeHead;
    public int maxPatternCount;
    private GameObject bullet;

    private bool TimeOut; //check player defeated the boss in time

    private void Awake()
    {
        InitialPos = this.transform.position;
        snakeHeadPos = snakeHead.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        motionIndex = 0;
        snakeHead.transform.position = snakeSitPos.transform.position;
    }

    public void StartSnake()
    {
        TimeOut = true;
        move();
    }

    private void move()
    {
        if (this.transform.position == ParryPos.position && snakeHead.transform.position == snakeHeadPos)
        {
            ParryShoot();
            StartCoroutine("bossResetTimer");
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, ParryPos.position, speed * Time.deltaTime);
        snakeHead.transform.position = Vector2.MoveTowards(snakeHead.transform.position, snakeHeadPos, speed * Time.deltaTime);

        Invoke("move", 0);
    }

    private void moveback()
    {
        if (this.transform.position == InitialPos)
        {
            if(TimeOut)
            {
                GoBack();
            }
            else if(!TimeOut)
            {
                MoveOn();
            }

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, InitialPos, speed * Time.deltaTime);
        snakeHead.transform.position = Vector2.MoveTowards(snakeHead.transform.position, snakeSitPos.position, speed * Time.deltaTime);

        Invoke("moveback", 0);
    }

    private void ParryShoot()
    {
        if (BossAI2.Instance.phase != BossAI2.Phases.snake) return;

        int roundNumA = 10;

        for (int i = 0; i < roundNumA; i++)
        {
            bullet = Instantiate(parry, transform.position, transform.rotation);
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumA), Mathf.Sin(Mathf.PI * 2 * i / roundNumA));
            rigid.velocity = dirVec * 5;
        }

        Invoke("ParryShoot", 0.7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.target)
        {
            if (GameManager.parryCombo >= 3)
            {
                TimeOut = false;
                BossAI2.Instance.BossHealth--;
                SoundManager.Instance.PlayBossHit();
                moveback();
            }
            else
            {
                BossAI2.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
            }
        }
    }

    IEnumerator bossResetTimer()
    {
        yield return new WaitForSeconds(5);

        if (TimeOut)
        {
            Debug.Log("startcoroutine");
            moveback();
        }
    }

    private void MoveOn()
    {
        if (motionIndex == 1)
        {
            BossAI2.Instance.phase = BossAI2.Phases.buffalo;
        }
        else if (motionIndex == 2)
        {
            BossAI2.Instance.phase = BossAI2.Phases.vulture;
        }
        else if (motionIndex == 3)
        {
            BossAI2.Instance.phase = BossAI2.Phases.died;
        }

        BossAI2.Instance.startPhase();
    }

    private void GoBack()
    {
        if (motionIndex == 1)
        {
            BossAI2.Instance.phase = BossAI2.Phases.wolf;
        }
        if (motionIndex == 2)
        {
            BossAI2.Instance.phase = BossAI2.Phases.buffalo;
        }
        if (motionIndex == 3)
        {
            BossAI2.Instance.phase = BossAI2.Phases.vulture;
        }

        BossAI2.Instance.startPhase();
    }
}
