using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventMessage : MonoBehaviour
{
    public TextMeshProUGUI messageText;

    public IEnumerator OpenMenu(string message)
    {
        messageText.text = message;
        gameObject.SetActive(true);

        yield return Utils.WaitForKeyPress(KeyCode.Mouse0);

        CloseMenu();

    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
