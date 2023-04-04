using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LowerLights : MonoBehaviour
{
    [SerializeField] Light2D globalLight;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            if (globalLight.intensity > 0.9)
            {
                globalLight.intensity = 0.25f;
            }
            else 
            {
                globalLight.intensity = 0.98f;
            }
        }
    }
}
