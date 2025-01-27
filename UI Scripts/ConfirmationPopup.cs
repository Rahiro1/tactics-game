using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmationPopup : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public delegate void FunctionOnYes();
    private FunctionOnYes yesFunc;

    public void OpenMenu(string message, FunctionOnYes yesFunc)
    {
        titleText.text = message;
        this.yesFunc = yesFunc;
        gameObject.SetActive(true);
    }

    public void OnYesClicked()
    {
        CloseMenu();
        yesFunc();
    }

    public void OnNoClicked()
    {
        yesFunc = null;
        CloseMenu();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
