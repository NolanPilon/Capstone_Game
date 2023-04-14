using UnityEngine;

public class BossAI3Hand : MonoBehaviour
{
    private Vector2 initalPos;
    private Vector2 nextPos;

    private void OnEnable()
    {
        initalPos = transform.position;
        nextPos = new Vector2(transform.position.x, transform.position.y - 3.0f);
    }

    private void Update()
    {
        if (BossAI3.Instance.phase == BossAI3.Phases.Phase1)
        {
            if (BossAI3.Instance.TargetInRange(this.gameObject))
            {
                Movehand();
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, initalPos, 2 * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossAI3.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
        }
    }

    private void Movehand()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPos, 5 * Time.deltaTime);
    }
}
