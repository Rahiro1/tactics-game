using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCOverallInentoryMenuManager : MonoBehaviour
{
    public Character selectedCharacter;
    public List<InventoryButton> inventoryBtnMgrList;
    public GameObject container;
    public GridLayoutGroup grid;
    public TabManager PrimaryTabManager;
    public int numberAcross;
     

    public void LoadOverallInventory()
    {
        float buttonSize = container.GetComponent<RectTransform>().rect.width / numberAcross;
        grid.cellSize = new Vector2(buttonSize, buttonSize);
        LoadTab(PrimaryTabManager);
    }

    public void LoadOverallInventoryWeaponType(EventEnums weaponTypeEE)
    {
        Define.WeaponType weaponType = weaponTypeEE.weaponType;
        int i = 0;
        foreach (InventoryButton button in inventoryBtnMgrList)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Item item in GameManager.Instance.playerData.GetOverallInventory())
        {
            if (item is Weapon weapon)
            {
                if (weapon.weaponType != weaponType)
                {
                    continue;
                }
            }
            else
            {
                continue;
            }
            inventoryBtnMgrList[i].DisplayItem(item, false);
            inventoryBtnMgrList[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void LoadOverallInventoryArmourType(EventEnums armourTypeEE)
    {
        Define.ArmourType armourType = armourTypeEE.armourType;
        int i = 0;
        foreach (InventoryButton button in inventoryBtnMgrList)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Item item in GameManager.Instance.playerData.GetOverallInventory())
        {
            if (item is Armour armour)
            {
                if (armour.allowedArmourUser != armourType)
                {
                    continue;
                }
            }
            else
            {
                continue;
            }
            inventoryBtnMgrList[i].DisplayItem(item, false);
            inventoryBtnMgrList[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void LoadOverallInventoryOtherType()
    {
        int i = 0;
        foreach (InventoryButton button in inventoryBtnMgrList)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Item item in GameManager.Instance.playerData.GetOverallInventory())
        {
            if (item is Weapon)
            {
                continue;
            }
            if (item is Armour)
            {
                continue;
            }
            inventoryBtnMgrList[i].DisplayItem(item, false);
            inventoryBtnMgrList[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void LoadOverallInventoryAllType()
    {
        int i = 0;
        foreach (InventoryButton button in inventoryBtnMgrList)
        {
            button.gameObject.SetActive(false);
        }
        foreach (Item item in GameManager.Instance.playerData.GetOverallInventory())
        {
            inventoryBtnMgrList[i].DisplayItem(item, false);
            inventoryBtnMgrList[i].gameObject.SetActive(true);
            i++;
        }
    }

        public void LoadTab(TabManager tabManager)
    {
        if (tabManager.selectedTab != null)
        {
            tabManager.OnTabSelected(tabManager.selectedTab);
        }
    }
}
