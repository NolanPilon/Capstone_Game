using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFall : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            rb.WakeUp();
        }
    }
}
