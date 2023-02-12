using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]private GameObject bossDoor;
    [SerializeField] private GameObject bossCam;
    [SerializeField] private GameObject mainCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            StartCoroutine(EnterBossRoom());
            Debug.Log("Fucks");
        }
    }



    private IEnumerator EnterBossRoom() 
    {
        bossDoor.SetActive(false);
        yield return new WaitForSecondsRealtime(0.3f);
        mainCam.SetActive(false);
        bossDoor.SetActive(true);
        bossCam.SetActive(true);
    }



}
