using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject bossDoor;
    [SerializeField] private Collider2D bossDoorTrigger;
    [SerializeField] private Camera MainCam;
    [SerializeField] private Transform bossRoomPos;
    [SerializeField] private int camSize;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            MainCam.gameObject.GetComponent<CameraController>().enabled = false;
            StartCoroutine(EnterBossRoom());
        }
    }
    private IEnumerator EnterBossRoom() 
    {
        bossDoor.SetActive(false);
        yield return new WaitForSecondsRealtime(0.3f);
        bossDoorTrigger.enabled = false;
        bossDoor.SetActive(true);
        MainCam.orthographicSize = camSize;
        MainCam.transform.position = bossRoomPos.transform.position;
    }
}
