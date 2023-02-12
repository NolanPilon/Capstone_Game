using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject directionalLight;

    public GameObject SettingPopUp;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        directionalLight.SetActive(true);
        Time.timeScale = 0f; // game is paused
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        directionalLight.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMap()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void Settings()
    {
        SettingPopUp.SetActive(true);
    }

    public void OnClickExit()
    {
        SettingPopUp.SetActive(false);
    }
}
