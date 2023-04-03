using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlusBtnControl : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private GameObject[] slots;

    public void Update()
    {
        if (slots[3].activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);

            Vector2 rect = new Vector2(0, 78);

            for (int i = 0; i < 4; i++)
            {
                if (slots[i].activeSelf)
                {
                    rect.y -= 65;
                }
            }
            rectTransform.anchoredPosition = rect;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int i = 0; i < 4; i++)
        {
            if (!slots[i].activeSelf)
            {
                slots[i].SetActive(true);
                break;
            }
        }
    }

}
