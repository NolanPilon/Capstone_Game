using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public GameObject bullet;
    private int curPatternCount; //how many we used current pattern
    public int maxPatternCount; //max count of each patterns
    public float speed = 5.0f;

    public void ShootBullet()
    {
        bullet = Instantiate(bullet, this.transform.position, this.transform.rotation);
        
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount / maxPatternCount), -1);
        rigid.AddForce(dirVec.normalized * speed, ForceMode2D.Impulse);

        curPatternCount++;

        if (!gameObject.activeSelf)
        {
            return;
        }
        else if (curPatternCount < maxPatternCount)
        {
            Invoke("ShootBullet", 0.25f);
        }
    }
}
