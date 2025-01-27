using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillNotification : MonoBehaviour
{
    public TextMeshProUGUI skillNameText;



    public void OpenMenu(string notificationText)
    {
        skillNameText.text = notificationText;
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    
}
