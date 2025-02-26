using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattalionAttackRange : Battalion
{
    
    public BattalionAttackRange(List<UnitController> enemyList, Define.BattalionData battalionData) : base(enemyList, battalionData)
    {
        battalionOrderType = Define.BattalionOrderType.WaitUntilRange;
    }

    public override void CheckForActivation()
    {
        if (isActive)
        {
            return;
        }

        if (CheckForRangeActivation())
        {
            ActivateBattalion();
        }
    }

    public bool CheckForRangeActivation()
    {
        foreach (UnitController unit in battalionUnits)
        {
            foreach(MapTileController tile in unit.GetActiveRange())
            {
                if(tile.occupied) 
                {
                    if (tile.getUnit().Character.unitAllignment == Define.UnitAllignment.Player)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
