using UnityEngine;
using System.IO;
using System;

//Data for saving
public class PlayerData
{
    // Name, level, date
    public string name;
    public int level;
}

public class DataManager : MonoBehaviour
{
    // singletone
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();    //new player data

    public string path;                //path of Json file
    public int nowSlot;                 //slot data can be identified by file name

    public int nowLevel;

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

    public void DeleteData()
    {
        File.Delete(path + nowSlot.ToString());
    }

    public void DataClear()
    {
        nowSlot = -1;
        nowPlayer = new PlayerData();
    }

    public GameObject[] FindGameObjectsWithName(string name)
    {
        GameObject[] gameObjects = GameObject.FindObjectsOfType<GameObject>();
        GameObject[] arr = new GameObject[gameObjects.Length];
        int FluentNumber = 0;
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == name)
            {
                arr[FluentNumber] = gameObjects[i];
                FluentNumber++;
            }
        }
        Array.Resize(ref arr, FluentNumber);
        return arr;
    }
}
