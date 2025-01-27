using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMenuManager : AbstractUnitMenu
{
    [SerializeField] private ItemUseMenu itemUseMenu;
    [SerializeField] private List<InventoryButton> itemButtons;
    private Define.MenuType InventoryType;


    public void LoadInventoryMenu(UnitController unit)
    {
        // some hard cosed numbers here;
        Character unitCharacter = unit.Character;
        Sprite backgroundSprite; 
        int i = 1;

        foreach (InventoryButton button in itemButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (Item item in unitCharacter.CharacterInventory)
        {
            if(InventoryType == Define.MenuType.InventoryMenuAttack)  // CONSIDER - could make this into a switch statement and in a seperate method?
            {
                if (item is Weapon weapon)
                {
                    
                } else
                {
                    continue;
                }
            }

            if (InventoryType == Define.MenuType.InventoryMenuHeal)
            {
                if (item is HealingMagic healingMagic)
                {

                }
                else
                {
                    continue;
                }
            }

            backgroundSprite = Database.Instance.otherItemButtonBackground;

            if (item is Weapon)
            {
                backgroundSprite = Database.Instance.weaponButtonBackground;
            }
            if (item is Armour)
            {
                backgroundSprite = Database.Instance.armourButtonBackground;
            }


            if (item == unitCharacter.EquippedWeapon)
            {
                ActivateInventoryButton(0, item, backgroundSprite,true);

            } else if ( item == unitCharacter.EquippedArmour)
            {
                ActivateInventoryButton(7, item, backgroundSprite,true);

            } else if (unitCharacter.EquippedWeapon == null && i == 1)
            {
                ActivateInventoryButton(0, item, backgroundSprite, false);
                i++;
            } else
            {
                ActivateInventoryButton(i, item, backgroundSprite, false);
                i++;
            }

        }

    }

    public void ActivateInventoryButton(int buttonNumber, Item item, Sprite backgroundSprite, bool IsEquipped)
    {
        itemButtons[buttonNumber].DisplayItem(item, IsEquipped);
        itemButtons[buttonNumber].backgroundImage.sprite = backgroundSprite;
        itemButtons[buttonNumber].gameObject.SetActive(true);
    }

    public override void OpenMenu(Define.MenuType menuToOpen, List<Define.MenuType> previousMenus, UnitController unit, Vector3 position)
    {
        this.previousMenus = previousMenus;
        menuUnit = unit;
        menuPosition = position;
        gameObject.transform.position = position + GameManager.Instance.tileMapGrid.cellSize.y * Vector3.down; 
        InventoryType = menuToOpen;
        LoadInventoryMenu(unit);
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

    public void OnInventoryButtonlicked(InventoryButton buttonManager)
    {
        if (InventoryType == Define.MenuType.InventoryMenu)
        {
            itemUseMenu.SetItem(buttonManager.item);
            mainGameMenuManager.OpenMenu(Define.MenuType.ItemUseMenu, previousMenus, menuUnit, menuPosition + GameManager.Instance.tileMapGrid.cellSize.x*Vector3.right);
        }

        if (InventoryType == Define.MenuType.InventoryMenuAttack)
        {
            // could error check for wrong item type here
            if (buttonManager.item is Weapon weapon)
            {
                menuUnit.Character.EquipWeapon(weapon);
                GameManager.Instance.OnAttackClicked();
                CloseMenu();
            }
            
        }

        if (InventoryType == Define.MenuType.InventoryMenuHeal)
        {
            // could error check for wrong item type here
            if (buttonManager.item is HealingMagic healingMagic)
            {
                menuUnit.Character.EquipHeal(healingMagic);
                GameManager.Instance.OnHealClicked();
                CloseMenu();
            }

        }

        // determine which button was clicked and the item associated
        // Perform button effect e.g. equip selected weapon
        // And/or enter attack / heal / other input state 
    }
}
