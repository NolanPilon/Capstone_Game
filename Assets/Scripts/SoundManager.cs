using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    private GameObject BGM;
    // Start is called before the first frame update
    /*    void Start()
        {
            if (!PlayerPrefs.HasKey("musicVolume"))
            {
                PlayerPrefs.SetFloat("musicVolume", 1);
                Load();
            }
            else
            {
                Load();
            }
        }*/

    private void Start()
    {
        BGM = GameObject.FindGameObjectWithTag("BGM");
    }
    public void ChangeVolume()
    {
        BGM.GetComponent<AudioSource>().volume = volumeSlider.value;
        //Save();
    }

/*    public void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }*/
}
