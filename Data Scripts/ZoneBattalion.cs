using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBattalion : Battalion
{
    private List<Vector3Int> activationZone;

    public ZoneBattalion(List<UnitController> enemyList, int battalionNumber, List<Vector3Int> activationZone) : base(enemyList, battalionNumber)
    {
        battalionOrderType = Define.BattalionOrderType.waitForMapZone;
        this.activationZone = activationZone;
    }

    public override void CheckForActivation()
    {
        if (isActive)
        {
            return;
        }

        if (CheckForZoneActivation())
        {
            ActivateBattalion();
        }
        
    }

    public bool CheckForZoneActivation()
    {
        LevelMapManager lmapManager = GameManager.Instance.levelMapManager;

        foreach (Vector3Int location in activationZone)
        {
            if(lmapManager.GetUnit(location).Character.unitAllignment == Define.UnitAllignment.Player)
            {
                return true;
            }
        }

        return false;
    }
}
