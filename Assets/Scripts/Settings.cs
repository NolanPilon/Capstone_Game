using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private GameObject SettingPopUp;
    [SerializeField] private Slider BGMVolumeSlider; // BGM controller
    private GameObject BGM;

    void Start()
    {
        BGM = GameObject.FindGameObjectWithTag("BGM");

        // Set slider value same as BGM volume
        BGMVolumeSlider.value = BGM.GetComponent<AudioSource>().volume;
    }

    public void ChangeVolume()
    {
        BGM.GetComponent<AudioSource>().volume = BGMVolumeSlider.value;
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayMenu();
        SettingPopUp.SetActive(false);
    }
}
