using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Slider volumeSlider; // BGM controller
    private GameObject BGM;

    void Start()
    {
        BGM = GameObject.FindGameObjectWithTag("BGM");

        DontDestroyOnLoad(BGM);

        // Set slider value same as BGM volume
        volumeSlider.value = BGM.GetComponent<AudioSource>().volume;
    }

    public void ChangeVolume()
    {
        BGM.GetComponent<AudioSource>().volume = volumeSlider.value;
    }
}
