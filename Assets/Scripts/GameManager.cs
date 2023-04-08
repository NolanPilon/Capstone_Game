using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static int parryCombo = 0;
    public static int playerHP;
    public static int collectables = 0;
    public static float playerSpeed;
    public static int TotalCombo;       //for display Level End Screen
    public bool BossDied;               //for trigger Level End Screen

    public int combo;
    public float speed;
    public static Vector2 respawnPoint = Vector2.zero;
    public static int progressPoint = 0;

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

        collectables = 0;
        progressPoint = 0;

        BossDied = false;
    }

    private void Update()
    {
        speed = playerSpeed;
        combo = parryCombo;

        if (playerHP <= 0) 
        {
            playerDie();
        }

        if (parryCombo == 3)
        {
            TotalCombo++;
        }
    }

    private void playerDie() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
