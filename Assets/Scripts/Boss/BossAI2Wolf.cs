using UnityEngine;

public class BossAI2Wolf : MonoBehaviour
{
    Vector2 InitalPos;

    private void Start()
    {
        InitalPos = this.transform.position;
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        InitialMove();
    }

    private void Initialization()
    {
        this.transform.position = InitalPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Initialization();
            gameObject.SetActive(false);
            BossAI2Snake.motionIndex = 1;
            BossAI2.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
            BossAI2.Instance.phase = BossAI2.Phases.snake;
            BossAI2.Instance.startPhase();
        }
        else if (collision.collider.name == "Tilemap")
        {
            move();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.boundary)
        {
            Initialization();
            gameObject.SetActive(false);
            BossAI2Snake.motionIndex = 1;
            BossAI2.Instance.phase = BossAI2.Phases.snake;
            BossAI2.Instance.startPhase();
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
