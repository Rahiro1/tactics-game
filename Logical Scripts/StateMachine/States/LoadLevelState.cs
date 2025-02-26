using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LoadLevelState : State
{
    // this state loads the map and all the units on the map, then sets the state to the recombat state
    // doesn't neccesserily need to be a state in it's own right

    public LoadLevelState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        // CONSIDER - Add loading screen

        LevelSO level = gameManager.Level; // consider making this private global
        
        //Tilemap tilemap = GameObject.Instantiate(level.aestheticTilemap, gameManager.tileMapGrid.transform);

        // load map, enemies and players
        gameManager.levelMapManager.LoadMap(level);
        //gameManager.aestheticTilemap = tilemap.gameObject;

        float Cellsize = gameManager.tileMapGrid.cellSize.x;
        level.tileMap.CompressBounds();
        BoundsInt mapBounds = level.tileMap.cellBounds;
        Vector3 centreBounds = (Vector3)mapBounds.center*Cellsize;
        /*Vector3 maxBounds = (Vector3)mapBounds.max*Cellsize;
        Vector3 minBounds = (mapBounds.min +Vector3.left + Vector3.down) * Cellsize;
        Vector2 lowerLeft = new Vector2(minBounds.x, minBounds.y);
        Vector2 lowerRight = new Vector2(maxBounds.x, minBounds.y);
        Vector2 upperLeft = new Vector2(minBounds.x, maxBounds.y);
        Vector2 upperRight = new Vector2(maxBounds.x, maxBounds.y);

        gameManager.camerabounds.points = new[] { upperRight, upperLeft, lowerLeft, lowerRight };
        */
        //gameManager.mainCamera.transform.position = gameManager.tileMapGrid.CellToWorld(new Vector3Int(Mathf.FloorToInt(level.mapWidth / 2), Mathf.FloorToInt(level.mapHeight / 2), 0));
        gameManager.mainCamera.transform.position = new Vector3(centreBounds.x, centreBounds.y, -10);
        gameManager.background = GameObject.Instantiate(level.background, new Vector3(centreBounds.x, centreBounds.y, -5), Quaternion.identity);

        AddNewCharacters(level);
        LoadGenerics(level);
        LoadCharacters(level);
        LoadBattaions(level);
        LoadMapEvents(level);
        LoadPlayers(level);
        LoadShops(level);

        // move to next state
        gameManager.SetState(new PreCombatState(gameManager));

        yield break;
    }

    private void AddNewCharacters(LevelSO level)
    {
        foreach(CharacterSO newCharacter in level.newCharacters)
        {
            gameManager.playerData.AddPlayerCharacter(new Character(newCharacter));
        } 
    }

    private void LoadMapEvents(LevelSO level)
    {
        foreach (Define.MapEventData eventData in level.mapEventList)
        {
            gameManager.AddMapEvent(eventData.mapEvent);
        }
    }

    private void LoadBattaions(LevelSO level)
    {
        foreach(Define.BattalionData battalion in level.battalions)
        {
            gameManager.AddBattalion(battalion);
        }
    }

    public void LoadGenerics(LevelSO level)
    {
        foreach (Define.GenericEnemyData unitData in level.GenericEnemyList)
        {
            gameManager.AddGenericUnit(unitData);
        }
    }

    public void LoadCharacters(LevelSO level)
    {
        foreach (Define.CharacterData unitData in level.levelCharacterList)
        {
            gameManager.AddCharacterUnit(new Character(unitData.character), unitData.position);
        }
    }

    public void LoadPlayers(LevelSO level)
    {
        int unitLimit = level.playerPositions.Count;
        int count = 0;
        foreach (int charID in level.forcedDeployments) //instantiate units for forced deployments
        {
            foreach(Character character in gameManager.playerData.PlayerCharacters)
            {
                if (character.characterID == charID)
                {
                    gameManager.AddCharacterUnit(character, level.playerPositions[count]);
                    
                }
            }
            count++;
        }


        foreach(Character character in gameManager.playerData.PlayerCharacters)
        {
            if (!level.forcedDeployments.Contains(character.characterID) && count < unitLimit)
            {
                gameManager.AddCharacterUnit(character, level.playerPositions[count]);
                count++;
            }
        }
    }

    public void LoadShops(LevelSO level)
    {
        // This is a bit crude
        if (!level.hasPrecombatPhase)
        {
            return;
        }
        if(gameManager.playerData.currentLevelArmoury == null)
        {
            gameManager.playerData.currentLevelArmoury = new Shop(level.armoury);
            gameManager.playerData.currentLevelArmoury.level = level.levelID;
        }
        if (gameManager.playerData.currentLevelShop == null)
        {
            gameManager.playerData.currentLevelShop = new Shop(level.shop);
            gameManager.playerData.currentLevelShop.level = level.levelID;
        }


        if (gameManager.playerData.currentLevelArmoury.level != level.levelID)
        {
            gameManager.playerData.currentLevelArmoury = new Shop(level.armoury);
            gameManager.playerData.currentLevelArmoury.level = level.levelID;
        }
        if (gameManager.playerData.currentLevelShop.level != level.levelID)
        {
            gameManager.playerData.currentLevelShop = new Shop(level.shop);
            gameManager.playerData.currentLevelShop.level = level.levelID;
        }
    }

    public void LoadBackground(LevelSO level)
    {
        
    }


}
