using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCShopManager : MonoBehaviour
{
    public List<ShopInventoryButton> shopButtons;
    private Shop shop;
    public TabManager primaryTabManager;
    public TextMeshProUGUI itemOwnedQuantityText, playerFundsText, playerFavourText;



    public void LoadShop(Shop shop)
    {
        this.shop = shop;
        LoadTab(primaryTabManager);
        playerFundsText.text = GameManager.Instance.playerData.PlayerGold.ToString() + "g";
        playerFavourText.text = GameManager.Instance.playerData.PlayerBonusXP.ToString();
        itemOwnedQuantityText.text = "--";
    }

    public void LoadTab(TabManager tabManager)
    {
        if (tabManager.selectedTab != null)
        {
            tabManager.OnTabSelected(tabManager.selectedTab);
        }
    }

    public void LoadWeapons(EventEnums weaponTypeEE)
    {
        Define.WeaponType weaponType = weaponTypeEE.weaponType;

        int i = 0;
        foreach (ShopInventoryButton button in shopButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (ShopItem shopItem in shop.shopItems)
        {
            if (shopItem.GetItem() is WeaponSO weaponSO)
            {
                if (weaponSO.weaponType != weaponType)
                {
                    continue;
                }
            }
            else
            {
                continue;
            }


            shopButtons[i].DisplayItem(shopItem);
            shopButtons[i].backgroundImage.sprite = Database.Instance.weaponButtonBackground;
            shopButtons[i].gameObject.SetActive(true);
            i++;

        }
    }

    public void LoadArmours(EventEnums armourTypeEE)
    {
        Define.ArmourType armourType = armourTypeEE.armourType;

        int i = 0;
        foreach (ShopInventoryButton button in shopButtons)
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


            shopButtons[i].DisplayItem(shopItem);
            shopButtons[i].backgroundImage.sprite = Database.Instance.armourButtonBackground;
            shopButtons[i].gameObject.SetActive(true);
            i++;

        }
    }

    public void LoadOther()     // TODO - change this to account for all other items 
    {
        int i = 0;
        foreach (ShopInventoryButton button in shopButtons)
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


            shopButtons[i].DisplayItem(shopItem);
            shopButtons[i].backgroundImage.sprite = Database.Instance.otherItemButtonBackground;
            shopButtons[i].gameObject.SetActive(true);
            i++;

        }
    }

    public void LoadAll()
    {

        int i = 0;  
        foreach (ShopInventoryButton button in shopButtons)
        {
            button.gameObject.SetActive(false);
        }

        foreach (ShopItem shopItem in shop.shopItems)
        {
            
            shopButtons[i].DisplayItem(shopItem);
            shopButtons[i].gameObject.SetActive(true);
            i++;

        }
    }



    public void OnShopButtonClicked(ShopInventoryButton button)
    {
        PlayerData playerData = GameManager.Instance.playerData;
        if (button.shopItem.cost <= playerData.PlayerGold && button.shopItem.timeCost <= playerData.PlayerBonusXP)
        {
            playerData.AdjustGold(button.shopItem.cost * -1);
            playerData.AdjustFavour(button.shopItem.timeCost * -1);
            playerData.AddInventoryItem(button.shopItem.GetItem().CreateItem());
            button.shopItem.quantity -= 1;
            playerFundsText.text = GameManager.Instance.playerData.PlayerGold.ToString() + "g";
            playerFavourText.text = GameManager.Instance.playerData.PlayerBonusXP.ToString();
            OnButtonHover(button);
            LoadTab(primaryTabManager);

        }
    }

    public void OnButtonHover(ShopInventoryButton button)
    {
        int itemCount = 0;
        // only looks through overall inventory, not character inventories

        foreach (Item item in GameManager.Instance.playerData.GetOverallInventory())
        {
            if (item.itemID == button.shopItem.itemID)
            {
                itemCount++;
            }
        }
        itemOwnedQuantityText.text = itemCount.ToString();
    }
}
