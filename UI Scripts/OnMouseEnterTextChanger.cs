using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class OnMouseEnterTextChanger : MonoBehaviour, IPointerEnterHandler
{
    public string textToChangeTo;
    public TextMeshProUGUI textToChange;

    public void OnPointerEnter(PointerEventData eventData)
    {
        textToChange.text = textToChangeTo;
    }
}
