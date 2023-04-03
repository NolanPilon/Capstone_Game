using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManagement : MonoBehaviour
{
    public GameObject[] slots;

    // Update is called once per frame
    void Update()
    {
    }

    public int CountSlots()
    {
        int count = 0;

        for (int i = 0; i< slots.Length; i++)
        {
            if (slots[i].activeSelf)
            {
                count++;
            }
        }

        return count;
    }
}
