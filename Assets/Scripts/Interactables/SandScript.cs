using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class SandScript : MonoBehaviour
{
    public GameObject sand;
    public GameObject particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            sand.SetActive(false);
            Instantiate(particles, transform.position, Quaternion.identity);
            CameraShake.Instance.OnShakeCameraPosition(0.2f, 0.2f);
        }
    }
}
