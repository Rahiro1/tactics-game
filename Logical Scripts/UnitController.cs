using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
using System;

public class UnitController : MonoBehaviour
{
    public int level;
    public string unitName;
    public Character Character;
    public int uniqueID;
    protected GameManager gameManager;
    [SerializeField]
    public int currentHP //TODO - sort out references to currentHP and redirect to character
    {
        get
        {
            return Character.currentHP;
        }
        protected set
        {
            Character.currentHP = value;
        }
    }
    public int currentArmour
    {
        get
        {
            return Character.currentArmour;
        }
        protected set
        {
            Character.currentArmour = value;
        }
    }
    public int remainingMovement;
    public Define.UnitAllignment unitAllignment; // CONSIDER change name to unitAllignmentLoad
    public int BattalionNumber;
    public Battalion battalion;
    public Define.AIType aIType;
    public Define.AIType activatedAIType;
    public bool isActivated;
    private List<MapTileController> moveRange;
    public Vector3Int Location { get; set; }
    public MapTileController LocationTile {
        get
        {
            return gameManager.levelMapManager.GetValue(Location);
        }
    }
    public Vector3Int startOfTurnLocation { get; set; }
    public List<MapTileController> path;
    public bool hasActed;
    public bool isEnemyRangeHighlighted = false; // CONSIDER removing this
    public bool IsAnimationFin;
    public bool IsDestroyed = false;
    [HideInInspector] public List<MapTileController> enemyRangeHighlighted;

    [SerializeField] private Slider healthbar;
    [SerializeField] private Image healthbarFill;
    [SerializeField] private TextMeshProUGUI armourText;

    //UI elements
    public Image unitSprite;
    public Image armourIcon;
    public TextMeshProUGUI armourValue;
    public Slider healthslider;
    public Image greyoutImage;


    // calculated stats

    public int UnitTotalGuard
    {
        get
        {
            return Character.Guard + gameManager.levelMapManager.GetValue(Location).GuardValue;
        }
    }

    public int UnitTotalAvoid
    {
        get
        {
            return Character.Avoid + gameManager.levelMapManager.GetValue(Location).AvoidValue;
        }
    }

    // basic stats - TODO - make sure these are needed and rename if they are

    protected int maxArmour
    {
        get
        {
            return Character.ModifiedArmour; // re look at these
        }
    }
    private int maxHP
    {
        get
        {
            return Character.HP.value; // re look at these
        }
    }
    private int Strength
    {
        get
        {
            return Character.ModifiedStrength;
        }
    }
    private int Magic
    {
        get
        {
            return Character.ModifiedMagic;
        }
    }
    private int offence
    {
        get
        {
            return Character.ModifiedOffence;
        }
    }
    private int defence;
    private int resistance;
    private int speed;


    public void SetHasActed(bool state)
    {
        hasActed = state;
        if(Character.unitAllignment == Define.UnitAllignment.Player && greyoutImage != null)
        {
            if (state == true)
            {
                greyoutImage.gameObject.SetActive(true);
            }
            else
            {
                greyoutImage.gameObject.SetActive(false);
            }
        }
        
    }

    private void Awake()
    {
        uniqueID = gameObject.GetInstanceID();
    }

    public void StartOfTurnReset()
    {
        startOfTurnLocation = Location;
        remainingMovement = Character.Move.value;
        SetHasActed(false);
    }

    public void ResetToStartOfTurn()
    {
        transform.position = gameManager.tileMapGrid.CellToWorld(startOfTurnLocation);
        gameManager.levelMapManager.RemoveUnit(Location);
        gameManager.levelMapManager.SetUnit(startOfTurnLocation, this); // maybe put this as a method in unitcontroller

        Location = startOfTurnLocation;
        SetPath(new List<MapTileController>());
        remainingMovement = Character.Move.value;
        SetHasActed(false);

        // make refresh method in playercontroler
        DeselectRange();
    }


    // battle methods

    public bool TakeDamage(int damage, int rending)
    {
        // TODO make sure unit is desroyed after moving and animations
        currentHP = Mathf.Clamp(currentHP - damage, 0, currentHP);
        currentArmour = Mathf.Clamp(currentArmour - rending,0,maxArmour);
        ActivateBattalion();
        UpdateHealthBar();
        // update display

        if (currentHP <= 0)
        {
            // TODO - permadeath difficulty option if true -> bool = true else false
            
            gameManager.RemoveUnit(this, true);
            
            return true;
        } else
        {
            return false;
        }

    }

    public void RecieveHeal(int healAmount, int armourHeal)
    {
        currentHP = Mathf.Clamp(currentHP + healAmount, 1, maxHP);
        currentArmour = Mathf.Clamp(currentArmour + armourHeal, 0, maxArmour);
        UpdateHealthBar();
    }


    
    public IEnumerator SetPathAndWait(List<MapTileController> path)
    {
        IsAnimationFin = false;
        if (Character.unitAllignment != Define.UnitAllignment.Player && path.Count != 0)
        {
            yield return gameManager.StartCoroutine(gameManager.cameraMovement.PanTo(Location, path[path.Count - 1].MapLocation));
        }

        SetPath(path);

        //Debug.Log("Animation Starting");

        while (!IsAnimationFin)
        {
            MoveAlongPath(gameObject);
            if(path.Count == 0)
            {
                IsAnimationFin = true;
            }
            yield return null;
        }
        //Debug.Log("Animation Finished");
    }
    public void SetPath(List<MapTileController> path)
    {
        this.path = path;
        int pathLength = path.Count - 1;
        if (pathLength != -1)
        {
            DeselectRange();
            //Debug.Log("Start: " + Location);
            gameManager.levelMapManager.RemoveUnit(Location);
            //Debug.Log(pathLength);
            gameManager.levelMapManager.SetUnit(path[pathLength].MapLocation, this);
            Location = path[pathLength].MapLocation;
            foreach(MapTileController tile in path)
            {
                remainingMovement -= tile.GetMoveCost(Character.unitType);
            }
            //Debug.Log("End" + Location);
            
        }
    }


    public void MoveAlongPath(GameObject mover)
    {
        float step = GameManager.Instance.aestheticMovementSpeed * Time.deltaTime;
        
        if (path.Count == 0)
        {
            return;
        }

        mover.transform.position = Vector3.MoveTowards(mover.transform.position, gameManager.tileMapGrid.CellToWorld(path[0].MapLocation), step);

        if (Vector3.Distance(mover.transform.position, gameManager.tileMapGrid.CellToWorld(path[0].MapLocation)) < 0.01f)
        {
            mover.transform.position = gameManager.tileMapGrid.CellToWorld(path[0].MapLocation);
            path.RemoveAt(0);
            if (enemyRangeHighlighted != null)
            {
                UnHighlightEnemyRange();
                HighlightEnemyRange();
            }
        }

    }

    public List<MapTileController> GetAttackPlusMoveRange()
    {
        List<MapTileController> attackRangeTiles = gameManager.rangefinder.GetTilesInMoveAndAttackRange(
            Character.Move.value,
            FindMaxWeaponRange(),
            LocationTile,
            Character.unitType,
            Character.unitAllignment);

        return attackRangeTiles;
    }

    public List<MapTileController> GetActiveRange()
    {
        if(aIType == Define.AIType.Wait)
        {
            return GetStaticAttackRange();
        }
        else // Consider switch case to return default move range
        {
            return GetAttackPlusMoveRange();
        }
    }

    public List<MapTileController> GetStaticAttackRange()
    {
        return gameManager.rangefinder.GetTilesInAttackRange(FindMaxWeaponRange(), LocationTile);
    }

    public List<MapTileController> GetEquippedAttackRange()
    {
        return gameManager.rangefinder.GetTilesInAttackRange(Character.EquippedWeapon.range, LocationTile);
    }



    public void DisplayRange()
    {
        moveRange = gameManager.rangefinder.GetTilesInRangeMoveCost(
           Character.Move.value,
           gameManager.levelMapManager.GetValue(Location),
           Character.unitType,
           Character.unitAllignment);

        foreach (MapTileController tile in moveRange)
        {
            tile.Highlight();
        }
    }

    // TODO - add method for highlighting battalion and for unhighlighting - create in gamemanager and call from maptilecontroller

    public void DeselectRange()
    {
        if (moveRange == null)
        {
            return;
        }
        foreach (MapTileController tile in moveRange)
        {
            tile.UnHighlight();
        }
        moveRange.Clear();
    }

    public void HighlightEnemyRange()
    {
        if (enemyRangeHighlighted == null)  // skip if range already highlighted, just for performance
        {
            enemyRangeHighlighted = GetActiveRange();
            foreach (MapTileController tile in enemyRangeHighlighted)
            {
                tile.HighlightAttackRange(uniqueID);
            }
        }

    }

    public void RefreshEnemyRange()
    {
        if (enemyRangeHighlighted != null)
        {
            if (enemyRangeHighlighted.Count > 0)
            {
                HighlightEnemyRange();
            }
        }
    }

    public void UnHighlightEnemyRange()
    {
        if (enemyRangeHighlighted == null)   // don't do function if enemy range not displayed -> I don't want to run GetAttackPlusMoveRange unneccesserily  
        {
            return;
        }

        foreach (MapTileController tile in enemyRangeHighlighted)
        {
            tile.UnHighlightAttackRange(uniqueID);
        }

        enemyRangeHighlighted = null;
    }


    // AI

    public IEnumerator ImplementAI()
    {
        yield return GameManager.Instance.StartCoroutine(gameManager.aIManager.TriggerAI(this));
        //Debug.Log("AI implemented");
    }

    public void ActivateBattalion()
    {
        if (!isActivated)
        {
            foreach(Battalion battalion in gameManager.battalionList)
            {
                if(battalion.battalionNumber == BattalionNumber)
                {
                    battalion.ActivateBattalion();
                }
            }
            RefreshEnemyRange();
            


            isActivated = true;
        }
    }

    public void ActivateSecondaryAI()
    {
        aIType = activatedAIType;
        isActivated = true;
    }


    public void UpdateHealthBar()
    {
        healthbar.maxValue = maxHP;  // make sure this is simple I have comined generic and char versions of this
        healthbar.value = currentHP;
        armourText.text = currentArmour.ToString();
        if (currentHP < maxHP)
        {
            healthbarFill.color = Color.green;
        }
        if (currentHP < maxHP / 2)
        {
            healthbarFill.color = Color.yellow;
        }
        if (currentHP < maxHP / 4)
        {
            healthbarFill.color = Color.red;
        }
    }



    public int FindMaxWeaponRange()  // CONSIDER this method could be moved to the character script
    {
        int maxWeaponRange = 0;

        foreach (Item item in Character.CharacterInventory)
        {
            if (item is Weapon weapon)
            {
                if (maxWeaponRange < weapon.range)
                {
                    maxWeaponRange = weapon.range;
                }
            }
        }

        return maxWeaponRange;
    }



    public IEnumerator PlayMapAttackAnimationStart(UnitController defenderUnit)
    {
        IsAnimationFin = false;
        Vector3Int defenderLocation = defenderUnit.Location;
        Vector3 unitWorldLocation = gameManager.tileMapGrid.CellToWorld(Location);
        Vector3 defenderWorldLocation = gameManager.tileMapGrid.CellToWorld(defenderLocation);
        Vector3 targetWorldLocation = unitWorldLocation + Vector3.Normalize(defenderWorldLocation - unitWorldLocation) * Define.MAPANIMATIONBATTLEDISTANCE;

        float volume = GameManager.Instance.settings.masterVolume; // TODO - add effects volume modifier

        float step = Define.MAPANIMATIONBATTLESPEED * Time.deltaTime;

        if (Character.EquippedWeapon.GetItemSO() is WeaponSO weaponSO)
        {
            if (weaponSO.onAttackAudio != null)
            {
                SoundManager.PlaySound(weaponSO.onAttackAudio, volume);     
            }
            else
            {
                SoundManager.PlaySound(Define.SoundType.Physical, volume);
            }

        }
        else
        {
            SoundManager.PlaySound(Define.SoundType.Physical, volume);      
        }


        while (!IsAnimationFin)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWorldLocation, step);

            if (Vector3.Distance(transform.position, targetWorldLocation) < 0.01f)
            {
                IsAnimationFin = true;
            }
            yield return null;
        }

        yield break;
    }

    public IEnumerator PlayMapAttackAnimationEnd()
    {
        if (this == null)
        {
            yield break;
        }

        IsAnimationFin = false;
        Vector3 targetWorldLocation = gameManager.tileMapGrid.CellToWorld(Location);

        float step = Define.MAPANIMATIONBATTLESPEED * Time.deltaTime;

        while (!IsAnimationFin)
        {
            if (this == null)
            {
                yield break;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetWorldLocation, step);

            if (Vector3.Distance(transform.position, targetWorldLocation) < 0.01f)
            {
                IsAnimationFin = true;
            }
            yield return null;
        }

        yield break;
    }



}
