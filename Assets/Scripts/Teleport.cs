using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform teleportLocation;
    [SerializeField] private Transform cameraTransform;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            cameraTransform.position = new Vector3(teleportLocation.position.x, teleportLocation.position.y, -10);
            collision.gameObject.transform.position = teleportLocation.position;
        }
    }
}
