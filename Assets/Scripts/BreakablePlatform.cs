using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public int wallHP = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && GameManager.parryCombo >= wallHP)
        {
            Destroy(gameObject);
        }
    }
}
