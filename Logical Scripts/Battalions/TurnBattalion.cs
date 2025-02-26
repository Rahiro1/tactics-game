using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBattalion : Battalion
{
    public int activationTurn;
    public TurnBattalion(List<UnitController> enemyList, Define.BattalionData battalionData) : base(enemyList, battalionData)
    {
        battalionOrderType = Define.BattalionOrderType.WaitForTurn;
        activationTurn = battalionData.activationTurn;
    }

    //int activationTurn;
    public override void CheckForActivation()
    {
        if (isActive)
        {
            return;
        }

        if (CheckForTurnActivation())
        {
            ActivateBattalion();
        }
    }

    public bool CheckForTurnActivation()
    {
        if(activationTurn == GameManager.Instance.turnNumber)
        {
            return true;
        }

        return false;
    }
}
