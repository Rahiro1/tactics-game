using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameMenuManager : MonoBehaviour
{
    protected List<Define.MenuType> previousMenus;
    [SerializeField] private AbstractUnitMenu unitMenu, inventoryMenu, inGameMenu, itemUseMenu;
    

    public void OpenMenu(Define.MenuType menuToOpen, List<Define.MenuType> menusOpenedFrom, UnitController unit, Vector3 position)
    {
        if(menusOpenedFrom.Count == 0)
        {
            menusOpenedFrom.Add(Define.MenuType.ExitMenu);
        }

        menusOpenedFrom.Add(menuToOpen);
        previousMenus = menusOpenedFrom;
        MenuSwitch(menuToOpen,menusOpenedFrom, unit, position);
        // add menu opened from to list of previous menus
        // open menu to open
    }

    public void CloseToPreviousMenu(UnitController unit, Vector3 position)
    {
        // open menu to close to, which is the last item in previosMenus
        previousMenus.RemoveAt(previousMenus.Count - 1);
        MenuSwitch(previousMenus[(previousMenus.Count - 1)], previousMenus, unit, position);
    }

    public void MenuSwitch(Define.MenuType menuToOpen,List<Define.MenuType> menusOpenedFrom, UnitController unit, Vector3 position)
    {

        switch (menuToOpen)
        {
            case Define.MenuType.UnitMenu:
                unitMenu.OpenMenu(menuToOpen, menusOpenedFrom, unit, position);
                break;
            case Define.MenuType.InventoryMenu:
            case Define.MenuType.InventoryMenuAttack:
            case Define.MenuType.InventoryMenuHeal:
                inventoryMenu.OpenMenu(menuToOpen, menusOpenedFrom, unit, position);
                break;
            case Define.MenuType.InGameMenu:
                inGameMenu.OpenMenu(menuToOpen, menusOpenedFrom, unit, position);
                break;
            case Define.MenuType.ItemUseMenu:
                itemUseMenu.OpenMenu(menuToOpen, menusOpenedFrom, unit, position);
                break;
            default:
                GameManager.Instance.OnExitMenusTrigger();
                break;
        }
    }

    public void ShowMenu()
    {
        switch (previousMenus[previousMenus.Count - 1]) // TODO - replace these switch statements with an if statement and a list of abstractUnitMenus
        {
            case Define.MenuType.ExitMenu:
                GameManager.Instance.OnExitMenusTrigger();
                break;
            case Define.MenuType.UnitMenu:
                unitMenu.gameObject.SetActive(true);
                break;
            case Define.MenuType.InventoryMenu:
            case Define.MenuType.InventoryMenuAttack:
            case Define.MenuType.InventoryMenuHeal:
                inventoryMenu.gameObject.SetActive(true);
                break;
            case Define.MenuType.ItemUseMenu:
                itemUseMenu.gameObject.SetActive(false);
                break;
            case Define.MenuType.InGameMenu:
                inGameMenu.gameObject.SetActive(true);
                break;
        }
    }

    public void OnCancelClicked()
    {
        if (previousMenus == null)
        {

        }

        switch(previousMenus[previousMenus.Count - 1]) // TODO - replace these switch statements with an if statement and a list of abstractUnitMenus
        {
            case Define.MenuType.ExitMenu:
                GameManager.Instance.OnExitMenusTrigger();
                break;
            case Define.MenuType.UnitMenu:
                unitMenu.CloseToPreviousMenu();
                break;
            case Define.MenuType.InventoryMenu:
            case Define.MenuType.InventoryMenuAttack:
            case Define.MenuType.InventoryMenuHeal:
                inventoryMenu.CloseToPreviousMenu();
                break;
            case Define.MenuType.ItemUseMenu:
                itemUseMenu.CloseToPreviousMenu();
                break;
            case Define.MenuType.InGameMenu:
                inGameMenu.CloseToPreviousMenu();
                break;
        }
    }

    public void CloseAllMenus()
    {
        unitMenu.CloseMenu();
        inventoryMenu.CloseMenu();
        inGameMenu.CloseMenu();
        itemUseMenu.CloseMenu();
        previousMenus.Clear();
    }
}
