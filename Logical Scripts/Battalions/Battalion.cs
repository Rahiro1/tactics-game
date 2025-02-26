using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battalion
{
    public List<UnitController> battalionUnits;
    public int battalionNumber;
    public bool isActive;
    public Define.BattalionOrderType battalionOrderType;

    public Battalion(List<UnitController> enemyList, Define.BattalionData battalionData)
    {
        battalionNumber = battalionData.battalionNumber;
        battalionUnits = new List<UnitController>();
        foreach(UnitController unit in enemyList)
        {
            if (unit.BattalionNumber == battalionNumber)
            {
                battalionUnits.Add(unit);
                unit.battalion = this;
            }
        }
        isActive = false;

        
    }

    public void ActivateBattalion()
    {
        foreach(UnitController unit in battalionUnits)
        {
            unit.ActivateSecondaryAI();
        }
        isActive = true;
    }

    public abstract void CheckForActivation();

    public void RemoveUnit(UnitController unit)
    {
        if (battalionUnits.Contains(unit))
        {
            battalionUnits.Remove(unit);
        }
    }

    public static Battalion CreateBattalion(List<UnitController> unitsInBattalion, Define.BattalionData battalionData)
    {
        Battalion newBattalion;
        switch (battalionData.battailionType)
        {
            case Define.BattalionOrderType.WaitUntilRange:
                newBattalion = new BattalionAttackRange(unitsInBattalion, battalionData);
                break;
            case Define.BattalionOrderType.WaitForTurn:
                newBattalion = new TurnBattalion(unitsInBattalion, battalionData);
                break;
            case Define.BattalionOrderType.WaitLeaderRange:
                newBattalion = new BattalionLeaderAttackRange(unitsInBattalion, battalionData);
                break;
            case Define.BattalionOrderType.waitForMapZone:
                newBattalion = new ZoneBattalion(unitsInBattalion, battalionData);
                break;
            default:
                newBattalion = new BattalionAttackRange(unitsInBattalion, battalionData); // default to Range as it requires the least information
                // TODO - add error
                break;
        }

        return newBattalion;
    }
}
