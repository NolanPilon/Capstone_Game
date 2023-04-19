using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float sec;
    public int min;
    public bool timerIsRuning = false;
    public Text timeText;

    private void Start()
    {
        timerIsRuning = true;
        sec = 0;
        min = 0;
    }

    private void Update()
    {
        if (timerIsRuning)
        {
            sec += Time.deltaTime;
            
            timeText.text = "Timer " + string.Format("{0:D2}:{1:D2}", min, (int)sec);

            if ((int)sec > 59)
            {
                sec = 0;
                min++;
            }
        }
    }
}
