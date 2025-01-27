using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCArmouryManager : MonoBehaviour
{
    public List<ArmouryInventoryButton> armouryButtons;
    private Shop shop;
    public TabManager primaryTabManager;
    public Sprite weaponButtonBackground;
    public Sprite armourButtonBackground;
    public Sprite otherItemButtonBackground;
    public TextMeshProUGUI itemOwnedQuantityText, playerFundsText;



    public void LoadArmoury(Shop shop)
    {
        this.shop = shop;
        LoadTab(primaryTabManager);
        playerFundsText.text = GameManager.Instance.playerData.PlayerGold.ToString() + "g";
        itemOwnedQuantityText.text = "--";
    }

    public void LoadTab(TabManager tabManager)
    {
        if(tabManager.selectedTab != null)
        {
            tabManager.OnTabSelected(tabManager.selectedTab);
        }
    }

    public void LoadWeapons(EventEnums weaponTypeEE)
    {
        Define.WeaponType weaponType = weaponTypeEE.weaponType;

        int i = 0;
        foreach (ArmouryInventoryButton button in armouryButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (ShopItem shopItem in shop.shopItems)
        {
            if(shopItem.GetItem() is WeaponSO weaponSO)
            {
                if(weaponSO.weaponType != weaponType)
                {
                    continue;
                }
            }
            else
            {
                continue;
            }


            armouryButtons[i].DisplayItem(shopItem);
            armouryButtons[i].backgroundImage.sprite = weaponButtonBackground;
            armouryButtons[i].gameObject.SetActive(true);
            i++;

        }
    }

    public void LoadArmours(EventEnums armourTypeEE)
    {
        Define.ArmourType armourType = armourTypeEE.armourType;

        int i = 0;
        foreach (ArmouryInventoryButton button in armouryButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (ShopItem shopItem in shop.shopItems)
        {
            if (shopItem.GetItem() is ArmourSO armourSO)
            {
                if (armourSO.allowedArmourUser != armourType)
                {
                    continue;
                }
            }
            else
            {
                continue;
            }


            armouryButtons[i].DisplayItem(shopItem);
            armouryButtons[i].backgroundImage.sprite = armourButtonBackground;
            armouryButtons[i].gameObject.SetActive(true);
            i++;

        }
    }

    public void LoadOther()     // TODO - change this to account for all other items 
    {
        int i = 0;
        foreach (ArmouryInventoryButton button in armouryButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (ShopItem shopItem in shop.shopItems)
        {
            if (shopItem.GetItem() is WeaponSO)
            {
                continue;
            }
            if (shopItem.GetItem() is ArmourSO)
            {
                continue;
            }


            armouryButtons[i].DisplayItem(shopItem);
            armouryButtons[i].backgroundImage.sprite = otherItemButtonBackground;
            armouryButtons[i].gameObject.SetActive(true);
            i++;

        }
    }

    public void LoadAll()
    {

        int i = 0;
        foreach (ArmouryInventoryButton button in armouryButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (ShopItem shopItem in shop.shopItems)
        {
            armouryButtons[i].DisplayItem(shopItem);
            armouryButtons[i].gameObject.SetActive(true);
            i++;

        }
    }



    public void OnArmouryButtonClicked(ArmouryInventoryButton button)
    {
        PlayerData playerData = GameManager.Instance.playerData;
        if(button.shopItem.cost <= playerData.PlayerGold)
        {
            playerData.AdjustGold(button.shopItem.cost*-1);
            playerData.AddInventoryItem(button.shopItem.GetItem().CreateItem());
            button.shopItem.quantity -= 1;
            playerFundsText.text = GameManager.Instance.playerData.PlayerGold.ToString() + "g";
            OnButtonHover(button);
            LoadTab(primaryTabManager);

        }
    }

    public void OnButtonHover(ArmouryInventoryButton button)
    {
        int itemCount = 0;
        // only looks through overall inventory, not character inventories

        foreach( Item item in GameManager.Instance.playerData.GetOverallInventory())
        {
            if(item.itemID == button.shopItem.itemID)
            {
                itemCount++;
            }
        }
        itemOwnedQuantityText.text = itemCount.ToString();
    }
}
 