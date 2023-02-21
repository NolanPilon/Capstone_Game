using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{

    public float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject fade;

    void Update()
    {
        if(GameManager.parryCombo >= 3)
        {
            if(timeBtwSpawns <=0)
            {
                GameObject instance = (GameObject)Instantiate(fade, transform.position, Quaternion.identity);
                Destroy(instance,0.5f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}
