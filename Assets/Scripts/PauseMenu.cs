using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public GameObject SettingPopUp;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // game is paused
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMap()
    {
        Time.timeScale = 1f;
        Destroy(GameObject.FindGameObjectWithTag("BGM"));
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
