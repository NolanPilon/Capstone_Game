using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SlotsSelect : MonoBehaviour
{
    public Text[] slotText;
    [SerializeField] private InputField[] NameInputField;

    public static bool[] savefile = new bool[4];

    // Start is called before the first frame update
    void Start()
    {
        //Check saving data
        for (int i = 0; i < 4; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))
            {
                savefile[i] = true;
                DataManager.instance.nowSlot = i;
                DataManager.instance.LoadData();
                NameInputField[i].text = DataManager.instance.nowPlayer.name;
                slotText[i].text = DataManager.instance.nowPlayer.name + " level " + DataManager.instance.nowPlayer.level;
            }
            else
            {
                slotText[i].text = "Empty";
            }
        }
        DataManager.instance.DataClear();   //reset values after check
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        if (savefile[number])
        {
            DataManager.instance.LoadData();
        }
    }

    public void SaveGame()
    {
        DataManager.instance.SaveData();
    }

    public void LoadGame()
    {
        if (savefile[DataManager.instance.nowSlot])
        {
            SceneManager.LoadScene("LevelMap");
        }
        else
        {
            Debug.Log("There's no file");
        }
    }

}
