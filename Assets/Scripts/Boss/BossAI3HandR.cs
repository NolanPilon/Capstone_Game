using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3HandR : MonoBehaviour
{
    [SerializeField] private GameObject ParryBullet;

    private void OnEnable()
    {
        StartCoroutine(StartShoot());
    }

    IEnumerator StartShoot()
    {
        yield return new WaitForSeconds(1.5f);
        ShootParry();
    }

    private void ShootParry()
    {
        GameObject bullet = Instantiate(ParryBullet, transform.position, transform.rotation);
        bullet.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        bullet.transform.rotation = this.transform.rotation;
        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(0, -1);
        rigid.velocity = dirVec * 5;

        Invoke("ShootParry", 1.5f);
    }
}
