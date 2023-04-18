using System.Collections;
using UnityEngine;

public class BossAI2Vulture : MonoBehaviour
{
    public float speed = 5;
    [SerializeField] private Rigidbody2D rigid;
    private Vector2 dirVec;
    private Vector2 InitialPos;

    private void Start()
    {
        InitialPos = this.transform.position;
        dirVec = new Vector2(1, 0);
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine("Timer");
        this.GetComponent<BulletAttack>().ShootBullet();
    }

    private void Update()
    {
        if (BossAI2.Instance.phase == BossAI2.Phases.vulture)
        {
            move();
        }
        else if (BossAI2.Instance.phase == BossAI2.Phases.snake)
        {
            Initialization();
        }
    }

    private void Initialization()
    {
        this.transform.position = InitialPos;
        dirVec = new Vector2(1, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.boundary)
        {
            dirVec = -1 * dirVec;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            move();
        }
    }

    public void move()
    {
        rigid.velocity = dirVec.normalized * 5;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        
        BossAI2Snake.motionIndex = 3;
        BossAI2.Instance.phase = BossAI2.Phases.snake;
        BossAI2.Instance.startPhase();
        gameObject.SetActive(false);
    }
}
