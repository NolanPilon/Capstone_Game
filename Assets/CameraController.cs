using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 5.0f;

    public GameObject player;

    // Check Camera shaking is on
    public bool IsOnShake { set; get; }

    private void Start()
    {
        //Set the start position of camera same with player
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10.0f);
    }

    private void Update()
    {
        if (IsOnShake == true) return;

        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(dir.x * cameraSpeed * Time.deltaTime, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }

}
