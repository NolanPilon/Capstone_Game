using UnityEngine;

public class BossAI3Hand : MonoBehaviour
{
    private Vector2 initalPos;
    private Vector2 nextPos;

    [SerializeField] private LayerMask playerLayer;

    private Vector2 size;
    private RectTransform rectTransform;
    private float height;
    private float width;

    private void OnEnable()
    {
        // to draw Overlapbox
        rectTransform = GetComponent<RectTransform>();
        height = rectTransform.rect.height * rectTransform.localScale.y;
        width = rectTransform.rect.width * rectTransform.localScale.x;
        size = new Vector2(width, height);

        // to move hand when the player comes near
        initalPos = rectTransform.transform.position;
        nextPos = new Vector2(initalPos.x, initalPos.y - 3.0f);
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
                rectTransform.transform.position = Vector2.MoveTowards(rectTransform.transform.position, initalPos, 2 * Time.deltaTime);
            }
        }

        if(BossAI3.Instance.phase != BossAI3.Phases.AttackPhase)
        {
            if (hitPlayer())
            {
                BossAI3.Instance.playerStat.takeDamage(BossAI3.Instance.target.transform.position - this.transform.position);
            }
        }
    }

    private void Movehand()
    {
        rectTransform.transform.position = Vector2.MoveTowards(rectTransform.transform.position, nextPos, 5 * Time.deltaTime);
    }

    private bool hitPlayer()
    {
        return Physics2D.OverlapBox(transform.position, size, 0.0f, playerLayer);
    }

    // check for the size of overlapbox
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireCube(transform.position, size);
    //}
}
