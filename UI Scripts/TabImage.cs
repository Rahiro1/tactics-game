using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class TabImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TabManager tabManager;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabSelected;
    public Image background;
    public GameObject controlledObject;
    public UnityEvent OnTabSelectedEvent;
    public UnityEvent OnTabDeselectedEvent;

    void Start()
    {
        tabManager.Subscribe(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabManager.OnTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabManager.OnTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabManager.OnTabExit(this);
    }

    public void OnSelected()
    {
        background.sprite = tabSelected;
        if(controlledObject != null)
        {
            controlledObject.SetActive(true);
        }
        if(OnTabSelectedEvent != null)
        {
            OnTabSelectedEvent.Invoke();
        }
    }

    public void OnDeselected()
    {
        background.sprite = tabIdle;
        if (controlledObject != null)
        {
            controlledObject.SetActive(false);
        }
        if (OnTabDeselectedEvent != null)
        {
            OnTabDeselectedEvent.Invoke();
        }
    }

    public void SetIdle()
    {
        background.sprite = tabIdle;
    }

    public void SetHover()
    {
        background.sprite = tabHover;
    }
}
