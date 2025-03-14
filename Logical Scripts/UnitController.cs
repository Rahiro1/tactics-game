using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using TMPro;
using System;

public class UnitController : MonoBehaviour
{
    public Character Character { get; protected set; }
    public Animator animator;
    protected AnimatorOverrideController animatorOverrideController;
    private int uniqueID;
    protected GameManager gameManager;
    private int remainingMovement;
    public int BattalionNumber { get; protected set; }
    public Battalion battalion { get; set; }
    public Define.AIType aIType { get; protected set; }
    public Define.AIType activatedAIType { get; protected set; }
    public bool isActivated;
    private List<MapTileController> moveRange;
    public Vector3Int Location { get; protected set; }
    public MapTileController LocationTile { get{ return gameManager.levelMapManager.GetValue(Location); }}
    protected Vector3Int startOfTurnLocation { get; set; }
    private List<MapTileController> path;
    public bool hasActed;
    private bool IsAnimationFin;
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

    // testing - will remove
    private int positionCount;
    private bool goingUp;




    // calculated stats
    // TODO - work terrain bonuses into the modifier system
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

    public void Update()
    {
        if (positionCount > 100)
        {
            goingUp = !goingUp;
            positionCount = 0;
        }

        if (goingUp)
        {
            gameObject.transform.position = transform.position + Vector3.up*0.0005f;
            positionCount++;
        }
        else
        {
            gameObject.transform.position = transform.position + Vector3.down * 0.0005f;
            positionCount++;
        }
    }

    // called at the start of every turn to 
    public void StartOfTurnReset()
    {
        startOfTurnLocation = Location;
        remainingMovement = Character.Move.GetModifiedValue();
        SetHasActed(false);
    }

    public void UndoToStartOfTurn()
    {
        transform.position = gameManager.tileMapGrid.CellToWorld(startOfTurnLocation);
        gameManager.levelMapManager.RemoveUnit(Location);
        gameManager.levelMapManager.SetUnit(startOfTurnLocation, this); // maybe put this as a method in unitcontroller

        Location = startOfTurnLocation;
        SetPath(new List<MapTileController>());
        remainingMovement = Character.Move.GetModifiedValue();
        SetHasActed(false);

        // make refresh method in playercontroler
        DeselectRange();
    }


    // battle methods

    public bool TakeDamage(int damage, int rending)
    {
        // TODO make sure unit is desroyed after moving and animations
        // CONSIDER making a characer.TakeDamage() method
        Character.currentHP = Mathf.Clamp(Character.currentHP - damage, 0, Character.currentHP);
        Character.currentArmour = Mathf.Clamp(Character.currentArmour - rending,0, Character.MaxArmour);
        ActivateBattalion();
        UpdateHealthBar();
        // update display

        GameEvents.Instance.TriggerSkills(Define.SkillTriggerType.TakeDamage);

        if (Character.currentHP <= 0)
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
        Character.currentHP = Mathf.Clamp(Character.currentHP + healAmount, 1, Character.MaxHP.GetModifiedValue());
        Character.currentArmour = Mathf.Clamp(Character.currentArmour + armourHeal, 0, Character.MaxArmour);
        UpdateHealthBar();
    }

    public void UseItem(Item item)
    {
        item.OnUse(this);
        SetHasActed(true);
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
        if (animator != null)
        {
            animator.SetBool("isMoving", true);
        }
        

        while (!IsAnimationFin)
        {
            MoveAlongPath(gameObject);
            if(path.Count == 0)
            {
                if (animator != null)
                {
                    animator.SetBool("isMoving", false);
                }
                
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
            Character.Move.GetModifiedValue(),
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
        return gameManager.rangefinder.GetTilesInAttackRange(Character.EquippedWeapon.BonusRange, LocationTile);
    }

    public void DisplayRange()
    {
        moveRange = gameManager.rangefinder.GetTilesInRangeMoveCost(
           Character.Move.GetModifiedValue(),
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
        int maxHP = Character.MaxHP.GetModifiedValue();
        healthbar.maxValue = maxHP;  // make sure this is simple I have comined generic and char versions of this
        healthbar.value = Character.currentHP;
        armourText.text = Character.currentArmour.ToString();
        if (Character.currentHP < maxHP)
        {
            healthbarFill.color = Color.green;
        }
        if (Character.currentHP < maxHP / 2)
        {
            healthbarFill.color = Color.yellow;
        }
        if (Character.currentHP < maxHP / 4)
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
                if (maxWeaponRange < weapon.BonusRange)
                {
                    maxWeaponRange = weapon.BonusRange;
                }
            }
        }

        return maxWeaponRange;
    }



    public IEnumerator PlayMapAttackAnimationStart(UnitController defenderUnit)
    {
        IsAnimationFin = false;
        float animationTime = 0;
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

        if (animator != null)
        {
            animator.SetInteger("attackOption", 1);
            animationTime = animator.GetCurrentAnimatorStateInfo(0).length;
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

        yield return new WaitForSeconds(Mathf.Max(0,animationTime));

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

            if (animator != null)
            {
                animator.SetInteger("attackOption", 0);
            }


            yield return null;
        }
        if (animator != null)
        {
            animator.SetInteger("attackOption", 0);
        }
        
        yield break;
    }



}
