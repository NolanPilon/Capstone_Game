using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3Laser : MonoBehaviour
{
    [SerializeField] LineRenderer lightOfSight;
    [SerializeField] float rotspeed;
    RaycastHit hitInfo;

    public void shootLaser()
    {
        if (BossAI3.Instance.phase != BossAI3.Phases.AttackPhase) return;

        transform.Rotate(rotspeed * Vector3.forward * Time.deltaTime);

        DrawRay(transform.position, transform.position + transform.right * 5);

        Invoke("shootLaser", 0);
    }

    private void DrawRay(Vector2 startPos, Vector2 endPos)
    {
        lightOfSight.SetPosition(0, startPos);
        lightOfSight.SetPosition(1, endPos);
    }
}
