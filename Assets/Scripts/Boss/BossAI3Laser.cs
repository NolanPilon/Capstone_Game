using UnityEngine;

public class BossAI3Laser : MonoBehaviour
{
    [SerializeField] LineRenderer lightOfSight;
    [SerializeField] float rotspeed;
    [SerializeField] LayerMask LayerDetection;
    float hitDistance = 5f;
    RaycastHit hit;

    public void OnEnable()
    {
        shootLaser();
    }

    public void shootLaser()
    {
        if (BossAI3.Instance.phase != BossAI3.Phases.AttackPhase) return;

        transform.Rotate(rotspeed * Vector3.forward * Time.deltaTime);

        //RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 5.0f, LayerDetection);

        //if (hitInfo.collider != null)
        //{
        //    if (hitInfo.collider.CompareTag("Player"))
        //    {
        //        Debug.Log("Player attacked");
        //    }
        //}

        DrawRay(transform.position, transform.position + transform.right * 5.0f);

        Invoke("shootLaser", 0);
    }

    private void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lightOfSight.SetPosition(0, startPos);
        lightOfSight.SetPosition(1, endPos);
    }
}
