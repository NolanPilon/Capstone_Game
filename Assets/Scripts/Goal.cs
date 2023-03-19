using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            LoadNextScene(sceneName);
        }
    }

    void LoadNextScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
}
