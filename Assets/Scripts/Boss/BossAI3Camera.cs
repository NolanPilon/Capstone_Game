using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI3Camera: MonoBehaviour
{
    [SerializeField] private Camera MainCam;
    [SerializeField] private Transform bossRoomPos;

    [SerializeField] private Transform target;

    private bool Action = false;
    // Update is called once per frame
    void Update()
    {
        if(!Action)
        {
            if (MainCam.transform.position == bossRoomPos.position)
            {
                target.position = new Vector2(bossRoomPos.position.x, bossRoomPos.position.y);
                Action = true;
            }
        }
    }
}
