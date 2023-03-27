using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject MoneyBag;
    [SerializeField] private GameObject parent;

    private bool isSpawned = false;
    private Vector2 spawnPosition;

    void Update()
    {
        if (parent || transform.position.x != 1000 && transform.position.y != 1000)
        {
            spawnPosition = getSpawnLocation();
            Debug.Log(spawnPosition);
        }
        else 
        {
            if (!isSpawned)
            {
                Instantiate(MoneyBag, spawnPosition, Quaternion.identity);
                print("HasSpawned");
                isSpawned = true;
            }
            Destroy(parent, 0.2f);
        }
    }

    Vector2 getSpawnLocation()
    {
        if (parent) 
        {
            Vector2 spawnPosition = new Vector2(parent.transform.position.x, parent.transform.position.y);
        }

    return spawnPosition;
    }
}
