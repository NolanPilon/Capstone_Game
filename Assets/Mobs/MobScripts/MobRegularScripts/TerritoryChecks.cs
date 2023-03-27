using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerritoryChecks : MonoBehaviour
{

    [SerializeField] GameObject TerrOwner;
    [SerializeField] Transform startLocation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            TerrOwner.GetComponent<PathFindingAir>().SetNewTarget(collision.transform.GetChild(5).transform);
            TerrOwner.GetComponent<EnemyBrainAir>().SetOnTerritory(true);
        }
     
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TerrOwner.GetComponent<EnemyBrainAir>().SetOnTerritory(false);
            TerrOwner.GetComponent<PathFindingAir>().SetNewTarget(startLocation);
        }
            
    }
}
