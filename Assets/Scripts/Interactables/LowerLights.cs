using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LowerLights : MonoBehaviour
{
    [SerializeField] Light2D globalLight;
    [SerializeField] Transform playerPos;

    private void Update()
    {
        if (playerPos.position.y < this.transform.position.y)
        {
            globalLight.intensity = 0.25f;
        }
        else
        {
            globalLight.intensity = 0.98f;
        }
    }
}
