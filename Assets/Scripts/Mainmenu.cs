using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    public Button start;
    public Sprite newStartsprite;
    private Sprite originalStartsprite;
    public Button exit;
    public Sprite newExitsprite;
    private Sprite originalExitsprite;
    public Button settings;
    public Sprite newSettingssprite;
    private Sprite originalSettingssprite;

    public GameObject SettingPopUp;

    // Initialize the original sprites
    private void Start()
    {
        originalStartsprite = start.image.sprite;
        originalExitsprite = exit.image.sprite;
        originalSettingssprite = settings.image.sprite;
    }

    // functions for start mouse event
    public void EnterMouseStart()
    {
        start.image.sprite= newStartsprite;
    }

    public void ExitMouseStart()
    {
        start.image.sprite = originalStartsprite;
    }

    // functions for exit mouse event
    public void EnterMouseExit()
    {
        exit.image.sprite = newExitsprite;
    }

    public void ExitMouseExit()
    {
        exit.image.sprite = originalExitsprite;
    }

    // functions for start mouse event
    public void EnterMouseSettings()
    {
        settings.image.sprite = newSettingssprite;
    }

    public void ExitMouseSettings()
    {
        settings.image.sprite = originalSettingssprite;
    }

    // functions for load the new scene
    public void OnClickStart()
    {
        SceneManager.LoadScene("LevelMap");
    }

    public void OnClickExit()
    {
        if (SettingPopUp.activeSelf)
        {
            SettingPopUp.SetActive(false);
        }
        else Application.Quit();
    }

    public void OnClickSettings()
    {
        SettingPopUp.SetActive(true);
    }

}
