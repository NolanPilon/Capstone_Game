using UnityEngine;
using UnityEngine.UI;

public class LoadControl : MonoBehaviour
{
    public static bool IsLoad;

    public void OnClickLoad()
    {
        IsLoad = true;

        GameObject[] slots = DataManager.instance.FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = true;
        }
    }
}
