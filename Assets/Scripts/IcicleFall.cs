using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcicleFall : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject DropParticle;

    private bool Played = false;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
  
    }

    // Update is called once per frame
    void Update()
    {
       if(rb.IsAwake())
        {
            DropParticle.SetActive(false);
            if(!Played)
            {
                SoundManager.Instance.Playicicle();
                Played = true;

            }
        }

       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            rb.WakeUp();
        }
    }
}
