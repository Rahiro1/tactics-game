using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TooltipItem : MonoBehaviour
{
    public RectTransform rectTransform;
    public Camera cam;

    public void ShowTooltip(Item item, Vector3 position)
    {
        //Vector2 mousePosition = Input.mousePosition;
        float pivotX = cam.WorldToScreenPoint(position).x / Screen.width;
        float pivotY = cam.WorldToScreenPoint(position).y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

        LoadTooltip(item);

        gameObject.SetActive(true);
    }

    public abstract void LoadTooltip(Item item);

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
