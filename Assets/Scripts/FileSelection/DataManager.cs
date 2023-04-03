using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

//Data for saving
public class PlayerData
{
    // Name, level, date
    public string name;
    public int level;
    public DateTime dateTime;
}

public class DataManager : MonoBehaviour
{
    // singletone
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();    //new player data

    public string path;                //path of Json file
    public int nowSlot;         //slot data can be identified by file name
    public DateTime nowdateTime;

    private void Awake()
    {
        #region singletone
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject); //need to be in every scene
        #endregion

        path = Application.persistentDataPath + "/" + "saving";
    }

    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);    //change Playerdata to Json type
        File.WriteAllText(path + nowSlot.ToString(), data);       //save Json file
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString()) ; //load Json file
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); //chand Json data to PlayerData type
    }

    public void DeleteData(int slot)
    {
        File.Delete(path + slot.ToString());
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }
}
