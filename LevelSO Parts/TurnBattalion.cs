using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnBattalion : Battalion
{
    public int activationTurn;
    public TurnBattalion(List<UnitController> enemyList, int battalionNumber, int activationTurn) : base(enemyList, battalionNumber)
    {
        battalionOrderType = Define.BattalionOrderType.WaitForTurn;
        this.activationTurn = activationTurn;
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
