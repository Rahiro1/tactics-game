using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipBasicSuppliedTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TooltipBasic tooltip;
    // may introduce gameobject to pivot off here
    public GameObject attachedObject; // determins position and gives the tooltip text
    public Vector3 offset;
    public ITooltipBasicSupplier tooltipSupplier;
    public float tooltipDelay = 0.5f;
    private WaitForSeconds waitCache;
    private IEnumerator coroutine;

    void Start()
    {
        waitCache = new WaitForSeconds(tooltipDelay);
        tooltip = GameManager.Instance.basicTooltip;
        tooltipSupplier = attachedObject.GetComponent<ITooltipBasicSupplier>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        coroutine = TooltipWait();
        StartCoroutine(coroutine);
    }

    public IEnumerator TooltipWait()
    {
        yield return waitCache;
        tooltip.ShowTooltip(tooltipSupplier?.TooltipHeader(), tooltipSupplier?.TooltipContent(), attachedObject.transform.position + offset);
        yield break;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(coroutine);
        tooltip.HideTooltip();
    }

}
