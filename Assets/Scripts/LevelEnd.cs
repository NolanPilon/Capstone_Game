using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{
    [SerializeField] private Text bestTime;
    [SerializeField] private Text nowTime;
    [SerializeField] private Text combo;
    [SerializeField] private Text collectible;

    private int BestTime;
    private int NowTime = 0;
    private int Combo;
    private int Collectible;

    private string PlayerName;
    private int NowLevel;

    public Animator transition;

    private void Start()
    {
        PlayerName = DataManager.instance.nowPlayer.name;
        NowLevel = DataManager.instance.nowLevel;
    }

    private void OnEnable()
    {
        BestTime = PlayerPrefs.GetInt("BestScore " + PlayerName + " " + NowLevel.ToString(), 5);

        UpdateValue();
    }

    private void UpdateValue()
    {
        //NowTime = timer;  //Need to Update after timer UI included

        if (NowTime > BestTime)
        {
            BestTime = NowTime;
            PlayerPrefs.SetInt("BestScore " + PlayerName + " " + NowLevel.ToString(), this.BestTime);
        }

        Combo = GameManager.TotalCombo;
        Collectible = GameManager.collectables;

        DisplayValue();
    }

    private void DisplayValue()
    {
        bestTime.text = BestTime.ToString();
        nowTime.text = NowTime.ToString();
        combo.text = Combo.ToString();
        collectible.text = Collectible.ToString();
    }

    //Map Button
    public void OnClickMap()
    {
        StartCoroutine(StartMap());
    }

    IEnumerator StartMap()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("LevelMap");
    }

    //Next Button
    public void OnClickNextLevel()
    {
        StartCoroutine(StartNextLevel());
    }

    IEnumerator StartNextLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Level" + (NowLevel+1).ToString());
    }
}
