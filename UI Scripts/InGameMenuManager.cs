using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuManager : AbstractUnitMenu
{
    public BattleStatusUI battleStatusUI;
    public void OnStatusClicked()
    {
        battleStatusUI.OpenMenu();
    }

    public void OnQuitClicked()
    {
        GameManager.Instance.confirmationPopup.OpenMenu("Quit Game?", Application.Quit);
    }

    public void OnEndTurnClicked()
    {
        GameManager.Instance.OnEndTurnClicked();
    }
    
    public override void OpenMenu(Define.MenuType menuToOpen, List<Define.MenuType> previousMenus, UnitController unit, Vector3 position)
    {
        this.previousMenus = previousMenus;
        menuUnit = unit;
        menuPosition = position;
        gameObject.transform.position = position;

        gameObject.SetActive(true);
    }

    public override void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    public override void CloseToPreviousMenu()
    {
        gameObject.SetActive(false);
        mainGameMenuManager.CloseToPreviousMenu(menuUnit, menuPosition);
    }
}
