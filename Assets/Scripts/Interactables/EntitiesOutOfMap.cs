using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesOutOfMap : MonoBehaviour
{
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
