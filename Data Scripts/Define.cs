using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public const int ENEMYDETECTIONRANGE = 100;
    public const int THIEFMAXRANGE = 30;
    public const int SPEEDTHRESHOLD = 4;
    public const float MAPANIMATIONBATTLESPEED = 6f;
    public const float MAPANIMATIONBATTLEDISTANCE = 0.5f;
    public const int DESTROYEDENEMYEXPMULTPLIER = 6;
    public const int WEXPMULTPLIER = 5;
    public const string SETTINGSRELATIVEPATH = "/settings.json";
    public const int MASTERVOLUMEDEFAULT = 1;
    public const int MAXSTATSLIDERVALUE = 30;
    public const int BASEHITRATEOFHANDDFH = 80;

    public enum GameMngrAction
    {
        loadLevel = 0,
        endLevel = 1
    }

    public enum MenuType
    {
        ExitMenu = 0,
        InGameMenu = 1,
        UnitMenu = 2,
        InventoryMenu = 3,
        CombatForcastMenu = 4,
        LevelUpMenu = 5,
        StatsScreen = 6,
        InventoryMenuAttack = 7,
        InventoryMenuHeal = 8,
        InventoryMenuItem = 9,
        TradeMenu = 11,
        ItemUseMenu = 12,
        NewGameMenu = 13,
        LoadGameMenu = 14,
        SaveGameMenu = 15
    }
    public enum UnitType // currently being used for both unit type and unit move type, consider changig. when changing this remember to change the scriptable objects assigned to the player data reference and the mapTileController switch statement
    {
        Foot=0,
        Armour=1,
        Water=2,
        Flying=3,
        Beast=4
    }

    public static string UnitTypeToText(UnitType unitType)
    {
        switch (unitType)
        {
            case UnitType.Foot:
                return "Foot";
            case UnitType.Armour:
                return "Armour";
            case UnitType.Water:
                return "Water";
            case UnitType.Flying:
                return "Flying";
            case UnitType.Beast:
                return "Beast";
            default:
                return "None";
        }
    }

    public enum WeaponType
    {
        none=0,
        Sword=1,
        Polearm=2,
        Axe=3,
        Bow=4,
        Elemental=5,
        Healing=6,
        Armour=7,
        Creation=8,
        Decay=9,
        Natural=10
    }

    public enum ArmourType
    {
        Cloth=0,
        Light=1,
        Medium=2,
        Heavy=3,
        LightBarding=4,
        MediumBarding=5,
        HeavyBarding=6,
        Hide=7,
        Dragonskin=8
    }

    public enum SkillTriggerType
    {
        BattleStart = 0,
        BattleEnd = 1,
        TakeDamage = 2,
        LevelStart = 3,
        LevelEnd = 4,
        None = 5
    }

    public enum UnitAllignment
    {
        Player=0,
        Enemy=1,
        Ally=2,
        Other=3
    }
    public static string UnitAllignmentToText(UnitAllignment unitAllignment)
    {
        switch (unitAllignment)
        {
            case UnitAllignment.Player:
                return "Player";
            case UnitAllignment.Enemy:
                return "Enemy";
            case UnitAllignment.Ally:
                return "Ally";
            case UnitAllignment.Other:
                return "Other";
            default:
                return "None";
        }
    }

    public enum AIType // to add new AI update the AI manager, including the switch statement
    {
        Wait=0,
        charge=1,
        AttackRange=2,
        Thief=3
    }

    public static string AITypeToText(AIType aIType)
    {
        switch (aIType)
        {
            case AIType.Wait:
                return "Wait";
            case AIType.charge:
                return "Charge";
            case AIType.AttackRange:
                return "Range";
            case AIType.Thief:
                return "Steal";
            default:
                return "None";
        }
    }

    public enum BattalionOrderType
    {
        WaitForTurn,
        WaitUntilRange,
        WaitLeaderRange,
        waitForMapZone
    }

    public static string OrderTypeToText(BattalionOrderType battalionOrderType)
    {
        switch (battalionOrderType)
        {
            case BattalionOrderType.WaitForTurn:
                return "Wait for turn";
            case BattalionOrderType.WaitUntilRange:
                return "Enter attack range";
            case BattalionOrderType.WaitLeaderRange:
                return "Enter leader's range";
            case BattalionOrderType.waitForMapZone:
                return "Wait in area";
            default:
                return "None";
        }
    }

    public enum TileType    // when adding to this list, please update the tileTypeToText method below as well as create a new tileReference asset 
    {
        grass = 0,
        water = 1,
        mountain = 2,
        forest = 3,
        building = 4
    }
    public static string TileTypeToText(TileType tileType)
    {
        switch (tileType)
        {
            case TileType.grass:
                return "Grass";
            case TileType.water:
                return "Water";
            case TileType.mountain:
                return "Mountain";
            case TileType.forest:
                return "Forest";
            case TileType.building:
                return "Building";
            default:
                return "None";
        }
    }


    public enum SoundType
    {
        Physical = 0,
        Magical = 1,
        Movement = 2,
        Event = 3,
        Menu = 4,
        Selected = 5,
        Critical = 6
    }

    /*public struct TerrainDifficultyValues
    {
        public int footDifficulty;
        public int armourDifficulty;
        public int waterDifficulty;
        public int flyingDifficulty;
    }*/

    [System.Serializable]
    public struct GenericEnemyData // can be used for any generic unit
    {
        public int level;
        public ClassSO unitClass;
        public UnitAllignment allignment;
        public AIType AIType;
        public AIType SecondaryAIType;
        public int battalionNumber;
        public Vector3Int position;
        public WeaponSO equippedWeapon;
        public ArmourSO equippedArmour;
        public List<ItemSO> startingInventory;
    }

    [System.Serializable]
    public struct CharacterData
    {
        public CharacterSO character;
        public Vector3Int position;
    }

    [System.Serializable]
    public struct BattalionData
    {
        public int battalionNumber;
        public BattalionOrderType battailionType;
        public int activationTurn;
        public List<Vector3Int> activationZone;
        public int leaderID;
    }

    [System.Serializable]
    public struct MapEventData
    {
        public Vector3Int location;
        public MapEventSO mapEvent;
        public bool canTrigger;
    }

    [System.Serializable]
    public struct ShopData
    {
        public ItemSO item;
        public int quantity;
        public int cost;
        public int timeCost;
    }

    [System.Serializable]
    public struct GenericWeaponIconData
    {
        public WeaponType weaponType;
        public Sprite genericWeaponSprite;
    }

    [System.Serializable]
    public struct GenericArmourIconData
    {
        public ArmourType armourType;
        public Sprite genericArmourSprite;
    }

    [System.Serializable]
    public struct SoundData
    {
        public SoundType soundType;
        public AudioClip AudioClip;
    }

    [System.Serializable]
    public struct StoryData
    {
        // just for breaking down the parts of the story in each StorySO
        public List<DialogueData> dialogueList;
        public StoryEventSO storyEvent;
    }

    [System.Serializable]
    public struct DialogueData
    {
        public CharacterSO characterOne;
        public CharacterSO characterTwo;
        public CharacterSO characterThree;
        public CharacterSO characterFour;
        public int speaker;
        public string text;
        
    }

    /*public struct WeaponRankData
    {
        public WeaponType weaponType;
        public int weaponRank;
        public int experiance;
        public int weaponMasteryLevel;
    }
    */
}
