using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timePlayed = 0;
    public bool timerIsRuning = false;
    public Text timeText;
    private void Start()
    {
        timerIsRuning = true;
    }

    private void Update()
    {
        if (timerIsRuning)
        {
            if (timePlayed < 99999)
            {
                timePlayed += Time.deltaTime;
                timeText.text= "Timer: " + timePlayed.ToString();
            }
        }
    }
}
