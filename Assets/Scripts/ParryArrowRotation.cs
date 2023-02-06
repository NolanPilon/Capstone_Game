using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryArrowRotation : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private float rotateSpeed = 2800.0f;

    void Update()
    {
        if (isActiveAndEnabled) 
        {
            transform.RotateAround(player.transform.position, Vector3.forward, rotateSpeed * Time.deltaTime);
        }
    }
}
