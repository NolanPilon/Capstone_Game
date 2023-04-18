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

    [SerializeField] Timer timer;

    private void Start()
    {
        PlayerName = DataManager.instance.nowPlayer.name;
        NowLevel = DataManager.instance.nowLevel;
    }

    private void OnEnable()
    {
        timer.timerIsRuning = false;

        NowTime = timer.min * 60 + (int)timer.sec;

        GameManager.Instance.updateTotalCollectables(GameManager.collectables);

        GetBestTime();
    }

    private void GetBestTime()
    {
        BestTime = PlayerPrefs.GetInt("BestScore " + PlayerName + " " + NowLevel.ToString(), NowTime);
        UpdateValue();
    }

    private void UpdateValue()
    {
        if (NowTime <= BestTime)
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
        int min = BestTime / 60;
        float sec = BestTime % 60;

        bestTime.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        nowTime.text = string.Format("{0:D2}:{1:D2}", timer.min, (int)timer.sec);
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
