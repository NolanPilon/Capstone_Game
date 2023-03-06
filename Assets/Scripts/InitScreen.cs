using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitScreen : MonoBehaviour
{
    public Animator transition;
    void Update()
    {
        StartCoroutine(LoadMenu());
    }

    IEnumerator LoadMenu() 
    {
        yield return new WaitForSeconds(2.5f);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainMenu");
    }
}
