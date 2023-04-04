using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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

    private void Update()
    {
        if (image.sprite == BgBlack)
        {
            NameInputField.SetActive(true);
            NameOutput.color = Color.white;

            if (DeleteControl.IsDelete)
            {
                //DataManager.instance.DeleteData(slot);
                NameInputField.GetComponent<InputField>().text = string.Empty;
                DeleteControl.IsDelete = false;
            }
        }
        else
        {
            NameInputField.SetActive(false);
            NameOutput.color = Color.black;
        }
    }

    public void OnSlotClick()
    {
        if (image.sprite != BgBlack)
        {
            slots = FindGameObjectsWithName("Slot");

            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].GetComponent<Image>().sprite = BgWhite;
            }
            image.sprite = BgBlack;
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
