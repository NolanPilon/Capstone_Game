using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3HandR : MonoBehaviour
{
    private Vector2 initalPos;
    private Vector2 nextPos;

    private void OnEnable()
    {
        StartCoroutine(StartShoot());
        initalPos = transform.position;
        nextPos = new Vector2(transform.position.x, transform.position.y - 3.0f);
    }

    private void Update()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BossAI3.Instance.playerStat.takeDamage(collision.transform.position - this.transform.position);
        }
    }

    IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(1.0f);
        ShootParry();
    }

    private void ShootParry()
    {
        GameObject bullet = Instantiate(BossAI3.Instance.ParryBullet, transform.position, transform.rotation);
        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        bullet.transform.rotation = this.transform.rotation;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(0, -1);
        rigid.velocity = dirVec * 5;

        Invoke("ShootParry", 1.5f);
    }

    private void Movehand()
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPos, 5 * Time.deltaTime);
    }
}
