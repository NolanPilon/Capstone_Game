using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class BossAI2Vulture : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rigid;
    private Vector2 dirVec;

    private void Start()
    {
        dirVec = new Vector2(1, 0);
        rigid = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine("Timer");
        this.GetComponent<BulletAttack>().ShootBullet();
    }

    private void Update()
    {
        move();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == BossAI2.Instance.boundary)
        {
            dirVec = -1 * dirVec;
        }
    }

    private void move()
    {
        rigid.velocity = dirVec.normalized * speed;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        gameObject.SetActive(false);
        BossAI2.Instance.motion[3] = false;
        BossAI2.Instance.motion[0] = true;
        BossAI2Snake.motionIndex = 4;
    }
}
