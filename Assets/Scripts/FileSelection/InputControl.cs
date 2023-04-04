using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputControl : MonoBehaviour, IPointerClickHandler
{
    float clickTime = 0;

    [SerializeField] private Button slot;
    [SerializeField] private Text NameInput;
    [SerializeField] private Text NameOutput;

    [SerializeField] private Selectable save;

    private void OnEnable()
    {
        NameOutput.text = "";
        NameInput.color = Color.white;
    }

    private void OnDisable()
    {
        this.GetComponent<InputField>().interactable = false;
        save.interactable = false;
    }

    void OnMousDoubleClick()
    {
        GameObject[] slots = DataManager.instance.FindGameObjectsWithName("Slot");

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Selectable>().interactable = false;
        }

        slot.GetComponent<Selectable>().interactable = true;

        this.GetComponent<InputField>().interactable = true;

        save.interactable = true;
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
}
