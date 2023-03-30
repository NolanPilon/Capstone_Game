using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatform : MonoBehaviour
{
    bool moving = false;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject tumbleweed;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Collider2D spawnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            spawnTrigger.enabled = false;
            moving = true;
            door.SetActive(true);
            Instantiate(tumbleweed, spawnPoint.position, Quaternion.identity);
        }
    }
    void Update()
    {
        if (moving && transform.position.y < 35.5f) 
        {
            transform.Translate(0, 2f * Time.deltaTime, 0);
        }
    }
}
