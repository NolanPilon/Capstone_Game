using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3Shooting : MonoBehaviour
{
    public int offset;

    public void shootBullet()
    {
        StartCoroutine(StartShootLeft());
        StartCoroutine(StartShootRight());
    }

    IEnumerator StartShootLeft()
    {
        yield return new WaitForSeconds(1.75f);
        ShootParryLeft();
    }

    IEnumerator StartShootRight()
    {
        yield return new WaitForSeconds(1.0f);
        ShootParryRight();
    }

    private void ShootParryLeft()
    {
        if (BossAI3.Instance.phase == BossAI3.Phases.AttackPhase) return;

        GameObject bullet = Instantiate(BossAI3.Instance.ParryBullet, BossAI3.Instance.BossHandL.transform.position, BossAI3.Instance.BossHandL.transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(0, -1);
        rigid.velocity = dirVec * 5;

        Invoke("ShootParryLeft", 1.5f);
    }
    private void ShootParryRight()
    {
        if (BossAI3.Instance.phase == BossAI3.Phases.AttackPhase) return;

        GameObject bullet = Instantiate(BossAI3.Instance.ParryBullet, BossAI3.Instance.BossHandR.transform.position, BossAI3.Instance.BossHandR.transform.rotation);
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(0, -1);
        rigid.velocity = dirVec * 5;

        Invoke("ShootParryRight", 1.5f);
    }

    public void ShootBulletEye()
    {
        GameObject[] bullet;
        bullet = new GameObject[8];

        for (int i = 0; i < 8; i++)
        {
            bullet[i] = Instantiate(BossAI3.Instance.ParryObject,
                        BossAI3.Instance.BossEye.transform.position + new Vector3(Mathf.Sin((Mathf.PI * 2)/8 * i) * offset, Mathf.Cos((Mathf.PI * 2) / 8 * i) * offset, 0),
                        BossAI3.Instance.BossEye.transform.rotation);
        }
    }
}
