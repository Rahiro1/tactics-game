using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class StatsScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject statsPanel;
    [SerializeField] private CharacterSummaryUI characterSummary;
    [SerializeField] private AdvancedStatsDisplay advancedStatsDisplay;
    [SerializeField] private BasicStatsDisplay basicStatsDisplay;
    [SerializeField] private SkillsPanel skillsPanel;
    [SerializeField] private WeaponRanksPanel weaponRanksPanel;
    [SerializeField] private PCInventoryMenuManager inventoryMenu;
    [SerializeField] private BattalionPanel battalionPanel;
    [SerializeField] private Image characterSprite;
    [SerializeField] private Button exitButton;


    // methods

    public void DisplayStatsScreen(UnitController characterUnit)
    {
        Character character = characterUnit.Character;

        characterSummary.LoadMenu(character);
        advancedStatsDisplay.LoadMenu(character);
        basicStatsDisplay.LoadMenu(character);
        skillsPanel.LoadMenu(character.GetAllskills());
        weaponRanksPanel.LoadMenu(character);
        inventoryMenu.LoadCharacterInventory(character);
        battalionPanel.LoadMenu(characterUnit);

        //exitButton.gameObject.SetActive(true);
        statsPanel.gameObject.SetActive(true); 
    }

    public void OnExitStatsClicked()
    {
        //exitButton.gameObject.SetActive(false);
        statsPanel.gameObject.SetActive(false); 
    }


}
