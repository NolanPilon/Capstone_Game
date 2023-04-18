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
            GameManager.Instance.updateTotalCollectables(GameManager.collectables);
            LoadNextScene(sceneName);
        }
    }

    void LoadNextScene(string sceneName) 
    {
        GameManager.progressPoint = 0;
        SceneManager.LoadScene(sceneName);
    }
}
