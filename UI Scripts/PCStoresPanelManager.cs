using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PCStoresPanelManager : MonoBehaviour
{
    // rename class to PCStoresPanelManager
    public PCUIMainMenuManager PCMainMenu;
    public PCArmouryManager armouryManager;
    public PCShopManager ShopManager;
    public TextMeshProUGUI storeNameText, storeOwnerNameText, speechText;
    public GameObject armouryFundsDisplay, armouryTabsManager, armouryOwnerImage, shopFundsDisplay, shopTabsManager, shopOwnerImage, switchToShopButton, switchToArmouryButton;

    public void OpenMenu()
    {

        SwitchToArmoury();
        gameObject.SetActive(true);
    }
    public void OnExitClicked()
    {
        CloseMenu();
    }

    private void LoadArmoury()
    {
        armouryManager.LoadArmoury(GameManager.Instance.playerData.currentLevelArmoury);
    }

    private void LoadShop()
    {
        ShopManager.LoadShop(GameManager.Instance.playerData.currentLevelShop);
    }

    public void CloseMenu()
    {
        PCMainMenu.OpenMenu();
        gameObject.SetActive(false);
    }

    public void SwitchToShop()
    {
        armouryManager.gameObject.SetActive(false);
        armouryTabsManager.SetActive(false);
        ShopManager.gameObject.SetActive(true);
        shopTabsManager.gameObject.SetActive(true);
        armouryOwnerImage.SetActive(false);
        shopOwnerImage.SetActive(true);
        switchToArmouryButton.SetActive(true);
        switchToShopButton.SetActive(false);
        armouryFundsDisplay.SetActive(false);
        shopFundsDisplay.SetActive(true);
        storeNameText.text = "Shop";
        storeOwnerNameText.text = "Shop Owner";
        speechText.text = "Please feel welcome to browse the shop.";
        LoadShop();
    }

    public void SwitchToArmoury()
    {
        armouryManager.gameObject.SetActive(true);
        armouryTabsManager.SetActive(true);
        ShopManager.gameObject.SetActive(false);
        shopTabsManager.gameObject.SetActive(false);
        armouryOwnerImage.SetActive(true);
        shopOwnerImage.SetActive(false);
        switchToArmouryButton.SetActive(false);
        switchToShopButton.SetActive(true);
        armouryFundsDisplay.SetActive(true);
        shopFundsDisplay.SetActive(false);
        storeNameText.text = "Armoury";
        storeOwnerNameText.text = "Armoury Owner";
        speechText.text = "Welcome to the armoury, what would you like?";
        LoadArmoury();
    }
}
