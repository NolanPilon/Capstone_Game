using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SaveControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image[] Slots;
    [SerializeField] private GameObject[] NameInputField;
    [SerializeField] private Text[] PlayerName;
    [SerializeField] private Sprite BgWhite;
    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0;i < 4; i++)
        {
            NameInputField[i].SetActive(false);
            PlayerName[i].color = Color.black;
            Slots[i].sprite = BgWhite;
        }

        GameObject[] slots = FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = true;
        }
    }

    GameObject[] FindGameObjectsWithName(string name)
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
