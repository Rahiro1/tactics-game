using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipBasicTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipBasic tooltip;
    // may introduce gameobject to pivot off here
    public GameObject positionObject;
    public Vector3 offset;
    public string headerText, contentText;
    public float tooltipDelay = 0.5f;
    private WaitForSeconds waitCache;
    private IEnumerator coroutine;

    void Start()
    {
        waitCache = new WaitForSeconds(tooltipDelay);
        tooltip = GameManager.Instance.basicTooltip;
        if(positionObject == null)
        {
            positionObject = gameObject;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        coroutine = TooltipWait();
        StartCoroutine(coroutine);
    }

    public IEnumerator TooltipWait()
    {
        yield return waitCache;
        tooltip.ShowTooltip(headerText, contentText, positionObject.transform.position + offset);
        yield break;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(coroutine);
        tooltip.HideTooltip(); 
    }
}
