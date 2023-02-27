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
    [SerializeField] private Text collectablesText;
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

    void Start()
    {
        //SetHealthText(200); //Need to get initial health from player class
        StartCoroutine("increaseTimeEachSecond");
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

        SetCollectablesText(0);             //Neeed to change the argument after collectable script done
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

    public void SetCollectablesText(int collectables)
    {
        collectablesText.text = "x" + collectables; 
    }

    public void SetHP()
    {
        int playerhp = 3 - GameManager.playerHP;

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
