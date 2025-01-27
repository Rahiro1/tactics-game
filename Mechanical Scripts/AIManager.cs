using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager 
{
    // there are known bugs with movement AI when all player units are surrounded
    private GameManager gameManager;
    private LevelMapManager lMapManager;
    private BattleManager battleManager;
    private Weapon weaponToUse;

    public AIManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
        lMapManager = gameManager.levelMapManager;
        battleManager = gameManager.BattleManager;
    }


    #region "Control Method"
    public IEnumerator TriggerAI(UnitController thisUnit) // could createw scriptable objects for this
    {
        //Debug.Log("Starting AI");
        Define.AIType aIType = thisUnit.aIType;
        switch (aIType)
        {
            case Define.AIType.Wait:
                yield return gameManager.StartCoroutine(WaitAI(thisUnit));
                break;
            case Define.AIType.AttackRange:
                yield return gameManager.StartCoroutine(AttackRangeAI(thisUnit));
                break;
            case Define.AIType.charge:
                yield return gameManager.StartCoroutine(ChargeAI(thisUnit));
                break;
            case Define.AIType.Thief:
                yield return gameManager.StartCoroutine(ThiefAI(thisUnit));
                break;
            default:
                break;
        }

        //Debug.Log("Ai complete");
        yield break;
    }
    #endregion

    #region "AIType Methods"
    private IEnumerator WaitAI(UnitController thisUnit)
    {
        
        UnitController target = DetermineAITargetSmartest(thisUnit, out Weapon weaponChoice);

        yield return gameManager.StartCoroutine(testMethod());
        yield return gameManager.StartCoroutine(AttackIfInRange(thisUnit, target, weaponChoice));
        yield break;
    }

    private IEnumerator AttackRangeAI(UnitController thisUnit)
    {
        int maxWeaponRange = thisUnit.FindMaxWeaponRange();
        UnitController target = DetermineAITargetSmartest(thisUnit, out Weapon weaponChoice);

        List<MapTileController> maxAttackRange = thisUnit.GetAttackPlusMoveRange();
        bool isTargetInRange = false;


        foreach( MapTileController tile in maxAttackRange)
        {
            if (lMapManager.GetUnit(tile.MapLocation) == target)
            {
                isTargetInRange = true;
            }
        }

        if (isTargetInRange)
        {
            yield return gameManager.StartCoroutine(ChargeAI(thisUnit));
        }

        yield break;
        // call rangefinder for range with movecost plus tiles equal to weapon range
        // if in range -> charge AI
    }

    private IEnumerator ChargeAI(UnitController thisUnit)
    {
        // TODO currently prefers ranged weapons 100% of the time, will need to change to smarter AI
        UnitController target = DetermineAITargetSmartest(thisUnit, out Weapon weaponChoice);
        yield return gameManager.StartCoroutine(thisUnit.SetPathAndWait(RangedCharge(thisUnit, target)));
        yield return gameManager.StartCoroutine(AttackIfInRange(thisUnit, target, weaponChoice));

        yield break;
        
    }

    private IEnumerator ThiefAI(UnitController thisUnit)
    {
        MapTileController targetTile = DetermineAITilePilligableClosest(thisUnit);
        yield return gameManager.StartCoroutine(thisUnit.SetPathAndWait(MoveSimple(thisUnit, targetTile)));
        LootTile(thisUnit);
        if(thisUnit.LocationTile == lMapManager.GetValue(gameManager.Level.thiefEscapeLocation))
        {
            gameManager.RemoveUnit(thisUnit, false);
        }
        yield break;
    }
    #endregion

    #region "Implementation Methods"
    private List<MapTileController> RangedCharge(UnitController thisUnit, UnitController target)
    {
        int maxWeaponRange = thisUnit.FindMaxWeaponRange();

        // determine target -> move close to target -> AttackIfInRange()

        UnitController targetController = target;
        MapTileController moverTile = lMapManager.GetValue(thisUnit.Location);
        MapTileController targetTile = lMapManager.GetValue(target.Location);
        MapTileController moveTo = moverTile;


        if (lMapManager.GetManhattenDistance(moverTile, targetTile) != maxWeaponRange)
        {
            List<MapTileController> range = gameManager.rangefinder.GetTilesInRangeMoveCost(thisUnit.Character.Move.value, moverTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment);

            foreach (MapTileController tile in range)
            {
                if (lMapManager.GetManhattenDistance(targetTile, tile) == maxWeaponRange)
                {
                    if (gameManager.pathfinder.Pathfind(moverTile, tile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value).Count == maxWeaponRange)
                    {
                        moveTo = tile;
                        continue;
                    }
                }
            }
            if (moverTile != moveTo)
            {
                return gameManager.pathfinder.Pathfind(moverTile, moveTo, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value);
            }
            else
            {
                return MoveSimpleCharge(thisUnit, lMapManager.GetValue(target.Location));
            }
        }
        else
        {
            return new List<MapTileController>();
        }
    }
    private List<MapTileController> MoveSimpleCharge(UnitController thisUnit, MapTileController targetTile)
    {

        List<MapTileController> currentPath = new List<MapTileController>();
        List<MapTileController> tempPath;
        MapTileController moverTile = lMapManager.GetValue(thisUnit.Location);
        MapTileController moveTo = moverTile;
        //int tempDistance;
        int currentDistance = lMapManager.GetManhattenDistance(moverTile, targetTile);


        // if not already adjacent
        if (currentDistance != 1)
        {
            List<MapTileController> range = gameManager.rangefinder.GetTilesInRangeMoveCost(thisUnit.Character.Move.value, moverTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment);
            // if player can be pathed to
            targetTile.occupied = false;
            if (gameManager.pathfinder.Pathfind(moverTile, targetTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value).Count != 0)
            {

                foreach (MapTileController tile in range)
                {
                    tempPath = gameManager.pathfinder.Pathfind(tile, targetTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value);
                    if (!tile.occupied && tempPath.Count != 0)
                    {
                        if (currentPath.Count == 0 || tempPath.Count < currentPath.Count)
                        {
                            currentPath = tempPath;
                            moveTo = tile;
                        }
                    }
                }
            }
            else
            {
                // else move to the closest tile by distance 
                foreach (MapTileController tile in range)
                {
                    //tempDistance = lMapManager.GetManhattenDistance(tile, targetTile);
                    tempPath = gameManager.pathfinder.Pathfind(tile, targetTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, Define.ENEMYDETECTIONRANGE);
                    if (!tile.occupied && tempPath.Count != 0) // changed from tempDistance
                    {
                        if (currentPath.Count == 0 || tempPath.Count < currentPath.Count)
                        {
                            currentPath = tempPath;
                            moveTo = tile;
                        }
                    }
                }
            }
            targetTile.occupied = true;
            if (moverTile != moveTo)
            {
                return gameManager.pathfinder.Pathfind(moverTile, moveTo, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value);
            } else
            {
                return new List<MapTileController>();
            }
        }
        else
        {
            return new List<MapTileController>();
        }
    }

    private List<MapTileController> MoveSimple(UnitController thisUnit, MapTileController targetTile)
    {

        int currentPathLength = 0;
        int tempPathLength;
        List<MapTileController> tempPath;
        MapTileController moverTile = lMapManager.GetValue(thisUnit.Location);
        MapTileController moveTo = moverTile;
        //int tempDistance;
        int currentDistance = lMapManager.GetManhattenDistance(moverTile, targetTile);


        // if not already on
        if (currentDistance != 0)
        {
            
            // if tile can be pathed to
            if (gameManager.pathfinder.Pathfind(moverTile, targetTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value).Count != 0)
            {
                moveTo = targetTile;
            }
            else
            {
                List<MapTileController> range = gameManager.rangefinder.GetTilesInRangeMoveCost(thisUnit.Character.Move.value, moverTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment);
                // else move to the closest tile by distance 
                foreach (MapTileController tile in range)
                {
                    //tempDistance = lMapManager.GetManhattenDistance(tile, targetTile);
                    tempPath = gameManager.pathfinder.Pathfind(tile, targetTile, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, Define.ENEMYDETECTIONRANGE);
                    tempPathLength = tempPath.Count;
                    if (!tile.occupied && tempPathLength != 0) // changed from tempDistance
                    {
                        if (currentPathLength == 0 || tempPathLength < currentPathLength)   // can replace current path with integer?
                        {
                            currentPathLength = tempPathLength;
                            moveTo = tile;
                        }
                    }
                }
            }
            if (moverTile != moveTo)
            {
                return gameManager.pathfinder.Pathfind(moverTile, moveTo, thisUnit.Character.unitType, thisUnit.Character.unitAllignment, thisUnit.Character.Move.value);
            }
            else
            {
                return new List<MapTileController>();
            }
        }
        else
        {
            return new List<MapTileController>();
        }
    }

    private UnitController DetermineAITargetClosest(UnitController thisUnit)
    {
        UnitController target = null;
        int previous = 1000;
        List<UnitController> targetList = new List<UnitController>();

        if (thisUnit.Character.unitAllignment == Define.UnitAllignment.Enemy)
        {
            targetList.AddRange(gameManager.playerList);
            targetList.AddRange(gameManager.allyList);
            targetList.AddRange(gameManager.otherList);

        }
        else
        {
            targetList.AddRange(gameManager.enemyList);
        }

        foreach (UnitController playerUnit in targetList)
        {
            if (lMapManager.GetManhattenDistance(lMapManager.GetValue(thisUnit.Location), lMapManager.GetValue(playerUnit.Location)) < previous)
            {
                target = playerUnit;
                previous = lMapManager.GetManhattenDistance(lMapManager.GetValue(thisUnit.Location), lMapManager.GetValue(playerUnit.Location));
            }
        }


        return target;
    }

    private UnitController DetermineAITargetSmartest(UnitController thisUnit, out Weapon weaponChoice)
    {
        // find all possible targets
        UnitController target = null;
        int aIWeight = int.MinValue;
        int tempAIWeight;
        List<UnitController> targetList = new List<UnitController>();
        List<UnitController> targetsInRange = new List<UnitController>();
        List<MapTileController> unitAttackRange = thisUnit.GetActiveRange();
        List<Vector3Int> attackRangeLocations = new List<Vector3Int>();

        //find list of all possible targets
        if (thisUnit.Character.unitAllignment == Define.UnitAllignment.Enemy)
        {
            targetList.AddRange(gameManager.playerList);
            targetList.AddRange(gameManager.allyList);
            targetList.AddRange(gameManager.otherList);

        }
        else
        {
            targetList.AddRange(gameManager.enemyList);
        }

        foreach(MapTileController tile in unitAttackRange)
        {
            attackRangeLocations.Add(tile.MapLocation);
        }

        // find targets in range from list of all possible targets
        foreach(UnitController unit in targetList)
        {
            if (attackRangeLocations.Contains(unit.Location))
            {
                targetsInRange.Add(unit);
            }
        }


        weaponChoice = thisUnit.Character.EquippedWeapon;
        // if no enemies in range return closest
        if (targetsInRange.Count == 0)
        {
            return DetermineAITargetClosest(thisUnit);
        }

        foreach (UnitController unit in targetsInRange)
        {
            foreach (Item item in thisUnit.Character.CharacterInventory)
            {
                if (item is Weapon weapon)
                {
                    tempAIWeight = battleManager.DetermineWeaponScore(thisUnit.Character, weapon, unit.Character);
                    if(tempAIWeight > aIWeight)
                    {
                        aIWeight = tempAIWeight;
                        target = unit;
                        weaponChoice = weapon;
                    }
                }
            }
        }

        return target;
    }

    private MapTileController DetermineAITilePilligableClosest(UnitController thisUnit)
    {
        bool IsLootTarget = false;
        MapTileController moverTile = lMapManager.GetValue(thisUnit.Location);
        List<MapTileController> pathToLoot = new List<MapTileController>();
        MapTileController bestTargetTile = null;
        int thisPathLength;
        int tempBestPathLength = 1000; // use a const for this
        foreach(Define.MapEventData eventData in gameManager.Level.mapEventList)
        {
            if (gameManager.mapEventsList.Contains(eventData.mapEvent) && eventData.mapEvent.canLoot)
            {
                pathToLoot = gameManager.pathfinder.Pathfind(moverTile, lMapManager.GetValue(eventData.location), thisUnit.Character.unitType, thisUnit.Character.unitAllignment, Define.THIEFMAXRANGE);
                thisPathLength = pathToLoot.Count;
                if (thisPathLength < tempBestPathLength && thisPathLength != 0)
                {
                    tempBestPathLength = thisPathLength;
                    bestTargetTile = lMapManager.GetValue(eventData.location);
                    IsLootTarget = true;
                }
            }
        }

        if (IsLootTarget)
        {
            return bestTargetTile;
        }
        else
        {
            return lMapManager.GetValue(gameManager.Level.thiefEscapeLocation);     // CONSIDER - looping through multiple exit points
        }
    }

    private IEnumerator AttackIfInRange(UnitController thisUnit, UnitController target, Weapon weaponToUse)
    {

        int distanceToTarget = lMapManager.GetManhattenDistance(lMapManager.GetValue(thisUnit.Location), lMapManager.GetValue(target.Location));
        //Debug.Log("attacker Loaction " + thisUnit.Location.ToString() + " defender Location" + target.Location.ToString());

        //Debug.Log("weapon to consider: " + weaponToUse.ToString());
        if (weaponToUse != null && weaponToUse.range >= distanceToTarget)
        {
            //Debug.Log("weapon being used: " + weaponToUse.ToString() + " targeting: " + target.ToString());
            thisUnit.Character.EquipWeapon(weaponToUse);
            yield return GameManager.Instance.StartCoroutine(battleManager.ConductBattle(thisUnit, target));
        }
    }

    // replaced by DetermineAITargetSmartest?
    private Weapon WeaponDecisionAI(UnitController thisUnit, UnitController target, int distanceToTarget)
    {
        // TODO - Finalise weapon decision AI
        Weapon weaponToUse = thisUnit.Character.EquippedWeapon; // could still be null
        int weaponPotentialDamage = battleManager.DeterminePotentialDamage(thisUnit.Character, weaponToUse, target.Character);

        foreach (Item item in thisUnit.Character.CharacterInventory)
        {
            if (item is Weapon weapon)
            {
                if (weapon.range >= distanceToTarget)
                {
                    if (battleManager.DeterminePotentialDamage(thisUnit.Character, weapon, target.Character) > weaponPotentialDamage)
                    {
                        weaponToUse = weapon;
                    }
                }
            }
        }

        return weaponToUse;
    }

    private void LootTile(UnitController thisUnit)
    {
        foreach (Define.MapEventData eventData in gameManager.Level.mapEventList)
        {
            if (eventData.location == thisUnit.Location && eventData.mapEvent.canLoot)
            {
                gameManager.RemoveMapEvent(eventData.mapEvent);
                eventData.mapEvent.ReplaceAestheticTile(eventData.location);
            }
        }
    }

    private IEnumerator testMethod()
    {
        //Debug.Log("Test method okay"); // TODO - figure out why this method is neccessary
        yield break;
    }

    
    #endregion
}
