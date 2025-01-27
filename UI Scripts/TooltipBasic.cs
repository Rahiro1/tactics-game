using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// Tooltip class based off of Game Dev Guide on youtube's tooltip video
/// </summary>
public class TooltipBasic : MonoBehaviour
{
    public TextMeshProUGUI headerText, contentText;
    public LayoutElement layoutElement;
    public RectTransform rectTransform;
    public int characterWrapLimit;
    public Camera cam;

    public void ShowTooltip(string header, string content, Vector3 position)
    {
        if (string.IsNullOrEmpty(header))
        {
            headerText.gameObject.SetActive(false);
        }
        else
        {
            headerText.text = header;
            headerText.gameObject.SetActive(true);
        }

        if (string.IsNullOrEmpty(content))
        {
            contentText.gameObject.SetActive(false);
        }
        else
        {
            contentText.text = content;
            contentText.gameObject.SetActive(true);
        }

        int headerLength = header.Length;
        int contentLength = content.Length;

        if(headerLength > characterWrapLimit || contentLength > characterWrapLimit)
        {
            layoutElement.enabled = true;
        }
        else
        {
            layoutElement.enabled = false;
        }

        //Vector2 mousePosition = Input.mousePosition;
        float pivotX = cam.WorldToScreenPoint(position).x / Screen.width;
        float pivotY = cam.WorldToScreenPoint(position).y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
