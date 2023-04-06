using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SlotControl : MonoBehaviour
{
    [SerializeField] int slot;
    [SerializeField] private GameObject NameInputField;
    [SerializeField] private Text NameOutput;
    [SerializeField] private Text NameInput;
    [SerializeField] private Sprite BgBlack;
    [SerializeField] private Sprite BgWhite;

    [SerializeField] private Image image;
    GameObject[] slots;

    [SerializeField] private Button load;   //load button

    [SerializeField]private Animator transition;    //for move to levelmap scene

    private void Update()
    {
        if (image.sprite == BgBlack)
        {
            if (DeleteControl.IsDelete)
            {
                //this.gameObject.SetActive(false);

                DataManager.instance.nowSlot = slot;
                DataManager.instance.DeleteData();
                NameInputField.GetComponent<InputField>().text = string.Empty;
                image.sprite = BgWhite;
                DeleteControl.IsDelete = false;
                SlotsSelect.savefile[slot] = false;
            }
            else if (SaveControl.IsSave)
            {
                image.sprite = BgWhite;
                DataManager.instance.nowSlot = slot;
                if (!SlotsSelect.savefile[slot])
                {
                    DataManager.instance.nowPlayer.level = 1;
                }
                DataManager.instance.nowPlayer.name = NameInput.text;
                DataManager.instance.SaveData();
                SaveControl.IsSave = false;
                SlotsSelect.savefile[slot] = true;
            }
            else
            {
                NameInputField.SetActive(true);
                NameOutput.color = Color.white;
            }

            DataManager.instance.DataClear();

            if (SlotsSelect.savefile[slot])
            {
                load.interactable = true;

                if (LoadControl.IsLoad)
                {
                    DataManager.instance.nowSlot = slot;
                    DataManager.instance.LoadData();
                    LoadControl.IsLoad = false;
                    image.sprite = BgWhite;
                    MoveToLevelMap();
                }
            }
        }
        else
        {
            NameInputField.SetActive(false);
            NameOutput.color = Color.black;

            load.interactable = false;

            if (SlotsSelect.savefile[slot])
            {
                DataManager.instance.nowSlot = slot;
                DataManager.instance.LoadData();
                NameOutput.text = DataManager.instance.nowPlayer.name + " level " + DataManager.instance.nowPlayer.level;
            }
            else
            {
                NameOutput.text = "Empty";
            }
        }
    }

    public void OnSlotClick()
    {
        if (image.sprite != BgBlack)
        {
            slots = DataManager.instance.FindGameObjectsWithName("Slot");

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].GetComponent<Image>().sprite = BgWhite;
            }
            image.sprite = BgBlack;
        }
    }

    public void MoveToLevelMap()
    {
        StartCoroutine(StartLevelMap());
    }

    IEnumerator StartLevelMap()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("LevelMap");
    }
}
