using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecombatUIManager : MonoBehaviour
{
    [SerializeField] private PCUIMainMenuManager pCUIMainMenu;

    public void OpenMenu()
    {
        pCUIMainMenu.OpenMenu();
    }

    public void CloseMenu()
    {
        GameManager.Instance.SetState(new PlayerTurnState(GameManager.Instance));
    }
}
