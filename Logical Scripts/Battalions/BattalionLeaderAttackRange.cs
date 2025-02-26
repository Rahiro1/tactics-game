using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattalionLeaderAttackRange : Battalion
{
    public UnitController LeaderUnit { get; private set; }

    public BattalionLeaderAttackRange(List<UnitController> enemyList, Define.BattalionData battalionData) : base(enemyList, battalionData)
    {
        battalionOrderType = Define.BattalionOrderType.WaitLeaderRange;
        foreach (UnitController unit in GameManager.Instance.enemyList)
        {
            if (unit.Character.characterID == battalionData.leaderID)
            {
                LeaderUnit = unit;
            }
        }
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
        if (LeaderUnit != null) // TODO - check if this works or should be a null check?
        {
            foreach (MapTileController tile in LeaderUnit.GetActiveRange())
            {
                if (tile.occupied)
                {
                    if (tile.getUnit().Character.unitAllignment == Define.UnitAllignment.Player)
                    {
                        return true;
                    }
                }
            }
        }
        else
        {
            foreach (UnitController unit in battalionUnits)
            {
                foreach (MapTileController tile in unit.GetActiveRange())
                {
                    if (tile.occupied)
                    {
                        if (tile.getUnit().Character.unitAllignment == Define.UnitAllignment.Player)
                        {
                            return true;
                        }
                    }
                }
            }
        }

        

        return false;
    }

}
