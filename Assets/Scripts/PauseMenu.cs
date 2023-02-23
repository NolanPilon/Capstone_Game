using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject SettingPopUp;
    [SerializeField] Button PauseButton;

    public void Update()
    {
        Color color = PauseButton.GetComponent<Image>().color;

        if (ParryBehavior.inParry)
        {
            color.a = 0.2f;
            PauseButton.GetComponent<Image>().color = color;
            PauseButton.GetComponent<Button>().interactable = false;
            return;
        }

        PauseButton.GetComponent<Button>().interactable = true;
        color.a = 1.0f;
        PauseButton.GetComponent<Image>().color = color;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (ParryBehavior.inParry) return;

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
        //Destroy(GameObject.FindGameObjectWithTag("BGM"));
        SceneManager.LoadScene("LevelMap");
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
