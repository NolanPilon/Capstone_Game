using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager gameManager;
    public static int parryCombo = 0;
    public static float playerSpeed;
    public int combo;
    public float speed;
    

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
    }
}
