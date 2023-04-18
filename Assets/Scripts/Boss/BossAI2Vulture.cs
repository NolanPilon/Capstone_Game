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
        //Initialization();
        StartCoroutine("Timer");
        this.GetComponent<BulletAttack>().ShootBullet();
        Move();
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
            Move();
        }
    }

    private void Move()
    {
        rigid.velocity = dirVec.normalized * speed;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        Initialization();
        gameObject.SetActive(false);
        BossAI2Snake.motionIndex = 3;
        BossAI2.Instance.phase = BossAI2.Phases.snake;
        BossAI2.Instance.startPhase();
    }
}
