using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackControl : MonoBehaviour
{
    public Animator transition;

    public void OnClickBack()
    {
        StartCoroutine(StartMainMenu());
    }

    IEnumerator StartMainMenu()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("MainMenu");
    }
}
