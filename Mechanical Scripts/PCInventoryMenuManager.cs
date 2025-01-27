using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PCInventoryMenuManager : MonoBehaviour
{

    // CONSIDER - possible architecture problem, this manager is resonsible for tasks that the panel manager should deal with - maybe move selected char and buttons there
    // CONSIDER - merging this with the other inventory menu manager 

    public Character selectedCharacter;
    public TextMeshProUGUI characterNameText;
    public List<InventoryButton> inventoryBtnList;
    public PCOverallInentoryMenuManager overallInventoryManager;

    public void LoadCharacterInventory(Character character)
    {
        selectedCharacter = character;
        if(characterNameText != null)
        {
            characterNameText.text = character.characterName;
        }
        Sprite backgroundSprite;

        int i = 1;
        foreach (InventoryButton button in inventoryBtnList)
        {
            button.gameObject.SetActive(false);
        }

        foreach (Item item in character.CharacterInventory)
        {
            backgroundSprite = Database.Instance.otherItemButtonBackground;
            if (item is Weapon)
            {
                backgroundSprite = Database.Instance.weaponButtonBackground;
            }
            if(item is Armour)
            {
                backgroundSprite = Database.Instance.armourButtonBackground;
            }

            // TODO - review this logic when I am less tired
            if (item == character.EquippedWeapon)
            {
                ActivateInventoryButton(0, item, backgroundSprite, true);

            }
            else if (item == character.EquippedArmour)
            {
                ActivateInventoryButton(7, item, backgroundSprite, true);

            }
            else if (character.EquippedWeapon == null && i == 1)
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

    public void ActivateInventoryButton(int buttonNumber, Item item, Sprite backgroundSprite, bool isEquipped)
    {
        inventoryBtnList[buttonNumber].DisplayItem(item, isEquipped);
        inventoryBtnList[buttonNumber].backgroundImage.sprite = backgroundSprite;
        inventoryBtnList[buttonNumber].gameObject.SetActive(true);
    }

    public void OnItemButtonClicked(InventoryButton buttonManager)
    {
        selectedCharacter.RemoveItemFromInventory(buttonManager.item);
        GameManager.Instance.playerData.AddInventoryItem(buttonManager.item);
        LoadCharacterInventory(selectedCharacter);
        overallInventoryManager.LoadTab(overallInventoryManager.PrimaryTabManager);
        // call method to update player Inventory display, possibly call the above line there?
    }

    public void OnOverallInventoryButtonClicked(InventoryButton buttonManager)
    {
        if(selectedCharacter.CharacterInventory.Count < 8)
        {
            GameManager.Instance.playerData.RemoveInventoryItem(buttonManager.item);
            selectedCharacter.AddItemToInventory(buttonManager.item);
            LoadCharacterInventory(selectedCharacter);
            overallInventoryManager.LoadTab(overallInventoryManager.PrimaryTabManager);
        }
        
    }

    
}
