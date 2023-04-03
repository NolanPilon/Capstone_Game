using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class SlotsSelect : MonoBehaviour
{
    public GameObject create;
    public Text[] slotText;
    public Text newPlayerName;

    bool[] savefile = new bool[4];

    public Button[] slot = new Button[4];

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
                slotText[i].text = DataManager.instance.nowPlayer.name;
            }
            else
            {
                slotText[i].text = "";
            }
        }
        DataManager.instance.DataClear();   //reset values after check
    }

    public void Slot(int number)
    {
        DataManager.instance.nowSlot = number;

        //if no data
        if (savefile[number])
        {
            DataManager.instance.LoadData();
            LoadGame();
        }
        else    //if have data -> load file and go to the game scene
        {
            Create();
        }

    }

    public void Create()
    {
        create.gameObject.SetActive(true);
    }

    public void LoadGame()
    {
        if (!savefile[DataManager.instance.nowSlot])
        {
            DataManager.instance.nowPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }
        SceneManager.LoadScene("Game");
    }

}
