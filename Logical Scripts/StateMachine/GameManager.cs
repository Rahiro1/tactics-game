using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Tilemaps;


public class GameManager : StateMachine
{
    public static GameManager Instance;

    public Camera mainCamera;
    public CameraMovement cameraMovement;
    public LevelSO Level { get; set; }
    public LevelMapManager levelMapManager;
    public Grid tileMapGrid;
    public GameObject background;

    // UI 
    public TitleScreenManager titleScreenManager;
    public PrecombatUIManager precombatUIManager;
    public MainGameMenuManager mainGameMenuManager;
    public Image inGameMenu;
    public Image inGameUnitMenu;
    public BattleForcastMenuManager battleForcast;
    public TradeMenuManager tradeMenuManager;
    public LevelUpScreenManager LevelUpScreenManager;
    public ExpDisplay expDisplay;
    public StatusWindowManager statusWindowManager;
    public ConfirmationPopup confirmationPopup;
    public GameObject finalVictoryScreen;
    public SkillNotificationDisplay skillNotificationDisplay;
    public EventMessage eventMessage;
    public TooltipBasic basicTooltip;
    public TooltipTerrain terrainTooltip;

    public Vector3 menuLocation;
    public GameObject characterPrefab;
    public GameObject genericUnitPrefab;
    [SerializeField] public StatsScreenManager StatsScreenManager;
    //public List<UnitController> precombatPlayerList = new List<UnitController>();
    public List<UnitController> enemyList = new List<UnitController>();
    public List<UnitController> playerList = new List<UnitController>();
    public List<UnitController> allyList = new List<UnitController>();
    public List<UnitController> otherList = new List<UnitController>();
    public List<Battalion> battalionList = new List<Battalion>();
    public List<MapEventSO> mapEventsList = new List<MapEventSO>();
    public PlayerData playerData { get; set; }
    public UnitController selectedPlayer;
    public Rangefinder rangefinder;                 // for determining unit move and attack range

    public Pathfinder pathfinder;                    // for calculating the correct path to a target tile from a tile
    public OverallSettings settings;
    public BattleManager BattleManager = new BattleManager();           // for managing battles and holds methods pertaining to battle
    public AIManager aIManager;                                         // for deserning between and implementing enemy AI
    public int aestheticMovementSpeed;
    public bool isEndState = false;    // when true signals to stop ongoing actions
    public int turnNumber;

    

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        aIManager = new AIManager(this);
        rangefinder = new Rangefinder(levelMapManager);
        pathfinder = new Pathfinder(levelMapManager);
        string path = Application.persistentDataPath + Define.SETTINGSRELATIVEPATH;
        if (File.Exists(path))
        {
            JSONDataService jSONDataService = new JSONDataService();
            settings = jSONDataService.LoadData<OverallSettings>(Define.SETTINGSRELATIVEPATH);
        }
        else
        {
            settings = new OverallSettings();
            settings.SetToDefaults();
            JSONDataService jSONDataService = new JSONDataService();
            jSONDataService.SaveData<OverallSettings>(Define.SETTINGSRELATIVEPATH, settings);
        }
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            OnCancel();
        }
    }

    public void StartLevel()
    {
        Level = Database.Instance.LevelDictionary[playerData.currentChapter];
        cameraMovement.OnLevelStart();
        turnNumber = 1;
        isEndState = false;
        SetState(new LoadLevelState(this));
    }

    public void EndLevel()
    {

        levelMapManager.UnloadMap();
        Destroy(background);
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            Destroy(enemyList[i].gameObject); // CONSIDER changing this to deal with garbage that may be left over
            Destroy(enemyList[i]);
            
        }
        for (int i = playerList.Count - 1; i >= 0; i--)
        {
            Destroy(playerList[i].gameObject);
            Destroy(playerList[i]);
            
        }
        for (int i = allyList.Count - 1; i >= 0; i--)
        {
            Destroy(allyList[i].gameObject);
            Destroy(allyList[i]);

        }
        for (int i = otherList.Count - 1; i >= 0; i--)
        {
            Destroy(otherList[i].gameObject);
            Destroy(otherList[i]);

        }
        //precombatPlayerList.Clear();
        enemyList.Clear();
        playerList.Clear();
        allyList.Clear();
        otherList.Clear();
        battalionList.Clear();
        mapEventsList.Clear();

        playerData.NextLevel();

        // TODO change this to implement inbetween level dialogue?

        // TODO implement final level win option -> credits etc
        StartLevel(); 
        
    }

    // statemachine methods - these mostly just call the relevent action in the currecnt state
    #region "Statemachine Methods"
    public void OnPlayerClicked(UnitController unit)
    {
        StartCoroutine(state.ClickPlayer(unit));
    }

    public void OnEnemyClicked(UnitController unit)
    {
        StartCoroutine(state.ClickEnemy(unit));
    }

    public void OnTileClicked(MapTileController tileClicked)
    {
        StartCoroutine(state.ClickEmptyTile(tileClicked));
    }

    public void OnAttackClicked()
    {
        SetState(new AttackState(this));
    }

    public void OnHealClicked()
    {
        SetState(new HealState(this));
    }

    public void OnTradeClicked()
    {

    }

    public void OnWaitClick()
    {
        StartCoroutine(state.ClickWait());
    }

    public void OnCancel()
    {
        if(state != null)
        {
            StartCoroutine(state.Cancel());
        }
        
    }

    public void OnEndTurnClicked()
    {
        StartCoroutine(state.EndTurn());
    }

    public void OnExitMenusTrigger()
    {
        StartCoroutine(state.ExitMenus());
    }
    public void OnRightClickUnit(UnitController unit)
    {
        StartCoroutine(state.RightClickUnit(unit));
    }
    #endregion



    public void OpenStatsScreen(UnitController unit) 
    {
        
        StatsScreenManager.DisplayStatsScreen(unit);
    }

    #region "Winning and Losing Methods"

    public void CheckForLevelEnd()
    {

        if (isEndState = CheckForVictory())
        {
            if (!Level.isFinalLevel)
            {
                SetState(new VictoryState(this));
            }
            else
            {
                SetState(new GameCompleteState(this));
            }

        }
        else if (isEndState = CheckForLoss())
        {
            SetState(new GameOverState(this));
        }

    }

    private bool CheckForLoss()
    {
        // TODO - implement other win/loss conditions
        if(playerList.Count == 0)
        {
            return true;
        }

        foreach (WinConditionSO condition in Level.defeatConditionsList)
        {
            if (condition.CheckForVictory())
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckForVictory()
    {
        // CONSIDER - adding default rout condition if victoryConditionsList is empty

        foreach(WinConditionSO condition in Level.victoryConditionsList)
        {
            if (condition.CheckForVictory())
            {
                return true;
            }
        }

        return false;
    }


    #endregion

    // setup method for creating the enounter, if GameManager gets cluttered, consider moving this to a seperate class?

    private void SetupLevel(LevelSO level)
    {
        // consider adding background
    }

    public void AddGenericUnit(Define.GenericEnemyData unitData)
    {
        MapTileController spawnLocation = FindNearestEmptyTile(levelMapManager.GetValue(unitData.position), unitData.unitClass.unitType);
        if (spawnLocation == null)
        {
            return;
        }

        GenericController unit = GameObject.Instantiate(genericUnitPrefab, tileMapGrid.CellToWorld(spawnLocation.MapLocation), Quaternion.identity).GetComponent<GenericController>();
        unit.InitialiseGeneric(unitData);
        switch (unitData.allignment)        // make this a method?
        {
            case Define.UnitAllignment.Player:
                // add player to playerlist
                playerList.Add(unit);
                break;
            case Define.UnitAllignment.Enemy:
                enemyList.Add(unit);
                break;
            case Define.UnitAllignment.Ally:
                allyList.Add(unit);
                break;
            case Define.UnitAllignment.Other:
                otherList.Add(unit);
                break;
            default:
                break;
        }
        levelMapManager.SetUnit(unitData.position, unit);
    } 

    public void AddCharacterUnit(Character character, Vector3Int mapPosition)
    {
        CharactersController unit = GameObject.Instantiate(characterPrefab, tileMapGrid.CellToWorld(mapPosition), Quaternion.identity).GetComponent<CharactersController>();
        unit.InitialiseCharacter(character, mapPosition);
        switch (character.unitAllignment)
        {
            case Define.UnitAllignment.Player:
                // add player to playerlist
                playerList.Add(unit);
                
                break;
            case Define.UnitAllignment.Enemy:
                enemyList.Add(unit);
                break;
            case Define.UnitAllignment.Ally:
                allyList.Add(unit);
                break;
            case Define.UnitAllignment.Other:
                otherList.Add(unit);
                break;
            default:
                break;
        }

        levelMapManager.SetUnit(mapPosition, unit);
    }

    internal void AddBattalion(Define.BattalionData battalion)
    {
        Battalion newBattalion;
        List<UnitController> unitsInBattalion = new List<UnitController>();

        foreach(UnitController unit in enemyList)
        {
            if(unit.BattalionNumber == battalion.BattalionNumber)
            {
                unitsInBattalion.Add(unit);
            }
        }

        switch (battalion.battailionType)
        {
            case Define.BattalionOrderType.WaitUntilRange:
                newBattalion = new BattalionAttackRange(unitsInBattalion, battalion.BattalionNumber);
                break;
            case Define.BattalionOrderType.WaitForTurn:
                newBattalion = new TurnBattalion(unitsInBattalion, battalion.BattalionNumber, battalion.activationTurn);
                break;
            case Define.BattalionOrderType.WaitLeaderRange:
                newBattalion = new BattalionLeaderAttackRange(unitsInBattalion, battalion.BattalionNumber, battalion.LeaderID);
                break;
            case Define.BattalionOrderType.waitForMapZone:
                newBattalion = new ZoneBattalion(unitsInBattalion, battalion.BattalionNumber,battalion.activationZone);
                break;
            default:
                newBattalion = new BattalionAttackRange(unitsInBattalion, battalion.BattalionNumber); // default to Range as it requires the least information
                break;
        }

        battalionList.Add(newBattalion);
    }

    internal void AddMapEvent(MapEventSO mapEvent)
    {
        mapEventsList.Add(mapEvent);
    }

    public void RemoveMapEvent(MapEventSO mapEventSO)       // for animations and tile changes, may have to add a friendly or enemy bool to know what to change to
    {
        if (mapEventsList.Contains(mapEventSO))
        {
            mapEventsList.Remove(mapEventSO);
        }

        // TODO implement tile altering once tile has been looted
        // add alternate tile sprite to mapeventSO
    }

    public void RemoveUnit(UnitController unit, bool IsPermanent)
    {
        switch (unit.Character.unitAllignment)
        {
            case Define.UnitAllignment.Player:
                playerList.Remove(unit);
                if (IsPermanent)
                {
                    playerData.RemovePlayerCharacter(unit.Character);
                }
                break;
            case Define.UnitAllignment.Enemy:
                enemyList.Remove(unit);
                break;
            case Define.UnitAllignment.Ally:
                allyList.Remove(unit);
                break;
            case Define.UnitAllignment.Other:
                otherList.Remove(unit);
                break;
            default:
                break;
        }
        unit.UnHighlightEnemyRange();
        if(unit.battalion != null)
        {
            foreach(Battalion battalion in battalionList)
            {
                if(battalion == unit.battalion)
                {
                    UnHighlightBattalion(unit);
                    battalion.battalionUnits.Remove(unit);
                    
                }
            }
        }
        Destroy(unit.gameObject);
        unit.IsDestroyed = true;
        levelMapManager.GetValue(unit.Location).RemoveUnit();

        CheckForLevelEnd();
    }

    internal void HighlightBattalion(UnitController occupyingUnit)
    {
        int battalionNumberToCheck = occupyingUnit.Character.battalionNumber;

        foreach(Battalion bat in battalionList)
        {
            if(bat.battalionNumber == battalionNumberToCheck)
            {
                foreach(UnitController unit in bat.battalionUnits)
                {
                    levelMapManager.GetValue(unit.Location).Highlight();
                }
            }
        }
    }

    internal void UnHighlightBattalion(UnitController occupyingUnit)
    {
        int battalionNumberToCheck = occupyingUnit.Character.battalionNumber;

        foreach (Battalion bat in battalionList)
        {
            if (bat.battalionNumber == battalionNumberToCheck)
            {
                foreach (UnitController unit in bat.battalionUnits)
                {
                    levelMapManager.GetValue(unit.Location).UnHighlight();
                }
            }
        }
    }

    public MapTileController FindNearestEmptyTile(MapTileController startTile, Define.UnitType unitMove)
    {
        int count = 0;

        if (!startTile.occupied)
        {
            return startTile;
        }

        while (count < 5)
        {
            foreach(MapTileController tile in rangefinder.GetTilesInAttackRange(count, startTile))
            {
                if(!tile.occupied && tile.GetMoveCost(unitMove) < 100) // CONDISER - removing hard coded number
                {
                    return tile;
                }
            }
            count++;
        }

        return null;
    }
}
