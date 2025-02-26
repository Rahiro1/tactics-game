using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData 
{
    // setters have to be public for loading to work - CONDISER - alternative it to make an alternate constructor for all variables

    public List<Character> PlayerCharacters { get; set; } // characters currently in player party
    public List<Character> RemovedPlayerCharacters { get; set; } // characters that were in the player party but have i.e. died
    public int PlayerGold { get; set; }
    public int PlayerBonusXP { get; set; }
    // TODO Inventory in playerData
    public List<Item> playerInventory { get; set; }

    public float timeOfLastSave;
    public float timePlayed;

    // difficulty options
    public bool IsEnemyTough { get; set; }
    public bool IsEnemySuperTough { get; set; }
    public bool IsEnemyStrong { get; set; }
    public bool IsEnemySuperStrong { get; set; }
    public bool IsEnemyPlentiful { get; set; }
    public bool IsEnemySuperPlentiful { get; set; }
    public bool IsEnemyReinforcementSlightlyRandom { get; set; }
    public bool IsEnemyReinforcementSuperRandom { get; set; }
    public bool IsEnemyPoor { get; set; }
    public bool IsEnemySuperPoor { get; set; }
    public bool IsPlayerExpLow { get; set; }
    public bool IsPlayerExpSuperLow { get; set; }



    // TODO add variable to track story progress
    public int currentChapter { get; set; }
    public int playerSaveFile; // which File the player has chosen to save to 
    public IDataService DataService = new JSONDataService();

    public Shop currentLevelArmoury;
    public Shop currentLevelShop;

    // TODO Save/Load/New game
    public void NewGame(int file)
    {
        // CONSIDER moving this elsewhere as this is logic, not data
        playerSaveFile = file;
        // save game 
        playerInventory = new List<Item>();
        PlayerGold = 1000;
        PlayerBonusXP = 500;
        timePlayed = 0;
        timeOfLastSave = Time.realtimeSinceStartup;
        // CurrentChapter = 1;
        // set difficulty options

        //  TODO create starting characters currently HARD CODED CONSIDER not hard coding character availability
        PlayerCharacters = new List<Character>();
        
        RemovedPlayerCharacters = new List<Character>();

        // make sure that gameManager and database are loaded on start if running this in a different scene



        currentChapter = 1;
        // start the game
        GameManager.Instance.StartLevel(); // change the number to current chapter

    }

    public void SaveGame(int file)
    {
        // TODO implement SaveGame file select
        if (!DataService.SaveData("SaveGame01.json", this)) // change save location based on selected save file
        {
            //Debug.Log("error saving");
        }
    }

    public PlayerData()
    {

    }

    public void AddPlayerCharacter(Character character)
    {
        PlayerCharacters.Add(character);
    }

    public void RemovePlayerCharacter(Character character)
    {
        PlayerCharacters.Remove(character);
        RemovedPlayerCharacters.Add(character);
    }

    public void NextLevel()
    {
        currentChapter++;
    }

    public void AddInventoryItem(Item item)
    {
        GetOverallInventory().Add(item);
    }

    public void RemoveInventoryItem(Item item)
    {
        GetOverallInventory().Remove(item);
    }

    public List<Item> GetOverallInventory()
    {
        if(playerInventory == null)
        {
            playerInventory = new List<Item>();
        }
        return playerInventory;
    }

    public void AdjustGold(int amount)
    {
        PlayerGold += amount;
    }

    public void AdjustFavour(int amount)
    {
        PlayerBonusXP += amount;
    }

    public float AdjustTime()
    {
        timePlayed += Time.realtimeSinceStartup - timeOfLastSave;
        timeOfLastSave = Time.realtimeSinceStartup;
        return timePlayed;
    }
}
