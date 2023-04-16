using UnityEngine;
using UnityEngine.UI;

public class DeleteControl : MonoBehaviour
{
    public static bool IsDelete;
    public void OnClickDelete()
    {
        IsDelete = true;

        SoundManager.Instance.PlayMenu();

        GameObject[] slots = DataManager.instance.FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = true;
        }
    }
}
