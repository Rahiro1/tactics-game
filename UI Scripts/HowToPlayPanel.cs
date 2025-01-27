using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayPanel : MonoBehaviour
{
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
