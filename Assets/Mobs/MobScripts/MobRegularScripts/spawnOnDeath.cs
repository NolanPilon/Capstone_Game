using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject MoneyBag;
    [SerializeField] private GameObject parent;

    private bool isSpawned = false;
    private bool isDead = false;
    private Vector2 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.transform.position.x != 1000 && parent.transform.position.y != 1000)
        {
            spawnPosition = getSpawnLocation();
        }
        else
        {
            isDead = true;
        }

            if (isDead)
            {
                 if(!isSpawned)
                 {
                     Instantiate(MoneyBag, spawnPosition, Quaternion.identity);
                     print("HasSpawned");
                     isSpawned = true;
                 }

                 Destroy(gameObject,0.2f);
            }
    }

    Vector2 getSpawnLocation()
    {

        Vector2 spawnPosition = new Vector2(parent.transform.position.x, parent.transform.position.y);

    return spawnPosition;
    }
}
