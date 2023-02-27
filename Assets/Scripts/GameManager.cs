using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static int parryCombo = 0;
    public static int playerHP;
    public static float playerSpeed;

    public int combo;
    public float speed;
    public static Vector2 respawnPoint = Vector2.zero;
    
    public static GameManager Instance { get { return gameManager; } }
    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this.gameObject);
        }
        else 
        {
            gameManager = this;
        }
    }

    private void Update()
    {
        speed = playerSpeed;
        combo = parryCombo;

        if (playerHP <= 0) 
        {
            playerDie();
        }
    }

    private void playerDie() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
