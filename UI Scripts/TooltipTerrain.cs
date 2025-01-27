using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipTerrain : MonoBehaviour
{
    public GameObject avoidDisplayObject, guardDisplayObject, regenerationDisplayObject;
    public TextMeshProUGUI terrainNameText, avoidValueText, guardValueText, regenerationValueText;
    public LayoutElement layoutElement;
    public int characterWrapLimit;

    public void ShowTooltip(string terrainName, int avoid, int guard, int regeneration)
    {
        if (string.IsNullOrEmpty(terrainName))
        {
            terrainNameText.gameObject.SetActive(false);
        }
        else
        {
            terrainNameText.text = terrainName;
            terrainNameText.gameObject.SetActive(true);
        }

        if (avoid == 0)
        {
            avoidDisplayObject.SetActive(false);
        }
        else
        {
            avoidValueText.text = avoid.ToString();
            avoidDisplayObject.SetActive(true);
        }

        if (guard == 0)
        {
            guardDisplayObject.SetActive(false);
        }
        else
        {
            guardValueText.text = guard.ToString();
            guardDisplayObject.SetActive(true);
        }

        if (regeneration == 0)
        {
            regenerationDisplayObject.SetActive(false);
        }
        else
        {
            regenerationValueText.text = guard.ToString();
            regenerationDisplayObject.SetActive(true);
        }



        int headerLength = terrainName.Length;

        if (headerLength > characterWrapLimit)
        {
            layoutElement.enabled = false;
        }
        else
        {
            layoutElement.enabled = true;
        }

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
