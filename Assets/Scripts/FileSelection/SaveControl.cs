using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image[] Slots;
    [SerializeField] private GameObject[] NameInputField;
    [SerializeField] private Text[] PlayerName;
    [SerializeField] private Sprite BgWhite;

    public static bool IsSave;

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSave = true;

        //for (int i = 0;i < 4; i++)
        //{
        //    NameInputField[i].SetActive(false);
        //    PlayerName[i].color = Color.black;
        //    Slots[i].sprite = BgWhite;
        //}

        GameObject[] slots = DataManager.instance.FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = true;
        }
    }
}
