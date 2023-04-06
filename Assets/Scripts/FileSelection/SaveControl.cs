using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveControl : MonoBehaviour
{
    public static bool IsSave;

    public void OnClickSave()
    {
        IsSave = true;

        GameObject[] slots = DataManager.instance.FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = true;
        }
    }
}
