using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Battalion
{
    public List<UnitController> battalionUnits;
    public int battalionNumber;
    public bool isActive;
    public Define.BattalionOrderType battalionOrderType;

    public Battalion(List<UnitController> enemyList, int battalionNumber)
    {
        this.battalionNumber = battalionNumber;
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
}
