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
    [SerializeField]
    private Image Live1;
    [SerializeField]
    private Image Live2;
    [SerializeField]
    private Image Live3;
    [SerializeField]
    private Text comboText;
    /*[SerializeField]
    private Text timeText;*/
    /*[SerializeField]
    private Text levelText;*/
    /*[SerializeField]
    Player player;*/

    public Sprite HeartAlive;
    public Sprite HeartDead;

    public bool isGameOver;
    private int time;

    void Start()
    {
        //SetHealthText(200); //Need to get initial health from player class
        StartCoroutine("increaseTimeEachSecond");
        Time.timeScale = 1;
        isGameOver = false;
    }

    private void Update()
    {
        if (GameManager.playerHP == 3)
        {
            AliveHeart(Live1);
            AliveHeart(Live2);
            AliveHeart(Live3);
        }
        else if (GameManager.playerHP == 2)
        {
            AliveHeart(Live1);
            AliveHeart(Live2);
            DeadHeart(Live3);
        }
        else if(GameManager.playerHP == 1)
        {
            AliveHeart(Live1);
            DeadHeart(Live2);
            DeadHeart(Live3);
        }
        else
        {
            DeadHeart(Live1);
            DeadHeart(Live2);
            DeadHeart(Live3);
        }

        SetCombotext(GameManager.parryCombo);
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

    public void SetCombotext(int combo)
    {
        comboText.text = "x" + combo;
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
