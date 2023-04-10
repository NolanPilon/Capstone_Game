using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    /*Reference for using at other class
    public GameUI gameUI;
    gameUI.SetHealthText(health);*/

    // Coding the UI
    [SerializeField] private Image[] Lives;
    [SerializeField] private Sprite HeartAlive;
    [SerializeField] private Sprite HeartDead;
    [SerializeField] private Image comboBar;
    /*[SerializeField]
    private Text timeText;*/
    /*[SerializeField]
    private Text levelText;*/
    /*[SerializeField]
    Player player;*/

    private bool isGameOver;
    private int time;
    private int playerhp;

    [SerializeField] private GameObject levelEndScreen;

    void Start()
    {
        //SetHealthText(200); //Need to get initial health from player class
        //StartCoroutine("increaseTimeEachSecond");
        Time.timeScale = 1;
        isGameOver = false;

        for (int i = 0; i < Lives.Length; i++)
        {
            AliveHeart(Lives[i]);
        }
    }

    private void Update()
    {
        SetHP();                            //Update the player HP UI

        SetCombo(GameManager.parryCombo);   //Update the parry combo bar
        
        SetCollectablesText(GameManager.collectables);  //Update the collectable text

        if (GameManager.Instance.BossDied)
        {
           levelEndScreen.SetActive(true);
        }
    }


    IEnumerator increaseTimeEachSecond()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(1);
            time += 1;
            //SetTimeText(time);
        }
        
    }

    public void SetCombo(int combo)
    {
        if (combo > 3) return; //update the bar until 3 combos

        comboBar.fillAmount = (float)combo / 3.0f;
    }
    public void SetHP()
    {
        playerhp = 3 - GameManager.playerHP;

        for (int i = 0; i < playerhp; i++)
        {
            DeadHeart(Lives[i]);
        }
    }

    public void AliveHeart(Image hp)
    {
        hp.sprite = HeartAlive;
    }
    public void DeadHeart(Image hp)
    {
        hp.sprite = HeartDead;
    }

}
