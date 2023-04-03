using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputControl : MonoBehaviour, IPointerClickHandler
{
    float clickTime = 0;

    [SerializeField] private Button slot;
    [SerializeField] private Text NameInput;
    [SerializeField] private Text NameOutput;

    private void OnEnable()
    {
        NameOutput.text = "";
        NameInput.color = Color.white;
    }

    private void OnDisable()
    {
        NameOutput.text = NameInput.text;
    }

    void OnMousDoubleClick()
    {        
        GameObject[] slots = FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = false;
        }

        slot.GetComponent<Selectable>().interactable = true;

        this.GetComponent<InputField>().interactable = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if ((Time.time - clickTime) < 0.3f)
        {
            OnMousDoubleClick();
            clickTime = -1;
        }
        else
        {
            clickTime = Time.time;
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
