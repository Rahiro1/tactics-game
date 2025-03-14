using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseMenu : AbstractUnitMenu
{
    private Item buttonItem;

    public override void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public override void CloseToPreviousMenu()
    {
        gameObject.SetActive(false);
        menuPosition += GameManager.Instance.tileMapGrid.cellSize.x * Vector3.left;
        mainGameMenuManager.CloseToPreviousMenu(menuUnit, menuPosition);
    }

    public override void OpenMenu(Define.MenuType menuToOpen, List<Define.MenuType> previousMenus, UnitController unit, Vector3 position)
    {
        this.previousMenus = previousMenus;
        menuUnit = unit;
        menuPosition = position;
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }

    public void SetItem(Item item)
    {
        buttonItem = item;
    }

    public void OnUseClicked()
    {
        if (buttonItem.IsUseable)
        {
            menuUnit.UseItem(buttonItem);
            mainGameMenuManager.CloseAllMenus();
            GameManager.Instance.SetState(new PlayerTurnState(GameManager.Instance));
        }
    }

    public void OnEquipClicked()
    {
        if (buttonItem is Weapon weapon)
        {
            menuUnit.Character.EquipWeapon(weapon);
            CloseToPreviousMenu();
        }
    }
}
