using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCOverallInventoryPanelManager : MonoBehaviour
{
    public PCUIMainMenuManager PCMainMenu;
    public UnitSelectionUIManager unitSelectionWindow;
    public PCInventoryMenuManager characterInventory;
    public PCOverallInentoryMenuManager overallInventory;
    public Image convoyOwnerIcon, convoyOwnerInformationImage;
    public Button returnToCharSelectButton;
    
    public void OpenMenu()
    {
        // disable what shouln't be displayed
        overallInventory.gameObject.SetActive(false);
        characterInventory.gameObject.SetActive(false);
        returnToCharSelectButton.gameObject.SetActive(false);

        // make sure everything that should be displayed is displayed
        convoyOwnerIcon.gameObject.SetActive(true);
        convoyOwnerInformationImage.gameObject.SetActive(true);

        // load the character selection
        unitSelectionWindow.LoadUnitView();
        unitSelectionWindow.gameObject.SetActive(true);
        
        //characterInventory.LoadCharacterInventory(GameManager.Instance.playerData.PlayerCharacters[0]) ; // auto load main character
        //overallInventory.LoadOverallInventory();
        gameObject.SetActive(true);
    }

    public void OnExitClicked()
    {
        CloseMenu();
    }

    public void CloseMenu()
    {
        PCMainMenu.OpenMenu();
        gameObject.SetActive(false);
    }

    public void ResolveCharacterButtonClicked(Character character)
    {
        // load what should be displayed
        characterInventory.LoadCharacterInventory(character); // auto load main character
        overallInventory.LoadOverallInventory();

        // disable what shouln't be displayed
        unitSelectionWindow.gameObject.SetActive(false);
        convoyOwnerIcon.gameObject.SetActive(false);
        convoyOwnerInformationImage.gameObject.SetActive(false);

        //enable what should be enabled
        characterInventory.gameObject.SetActive(true);
        overallInventory.gameObject.SetActive(true);
        returnToCharSelectButton.gameObject.SetActive(true);

    }

    public void OnCharacterButtonClicked(CharDisplayButton buttonManager)
    {
        ResolveCharacterButtonClicked(buttonManager.assignedCharacter);
    }

    public void OnReturnToCharacterSelectClicked()
    {
        OpenMenu();
    }
}
