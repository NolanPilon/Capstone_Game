using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]private GameObject bossDoor;
    [SerializeField] private Camera bossCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            bossCam.gameObject.GetComponent<CameraController>().enabled = false;
            StartCoroutine(EnterBossRoom());
        }
    }
    private IEnumerator EnterBossRoom() 
    {
        bossDoor.SetActive(false);
        yield return new WaitForSecondsRealtime(0.3f);
        bossDoor.SetActive(true);
        bossCam.orthographicSize = 8;
        bossCam.transform.position = new Vector3(107.5f, -72f, -1f);
    }
}
