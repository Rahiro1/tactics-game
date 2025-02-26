using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/LevelEnd/Victory/Turn Victory")]
public class WinConditionTurns : WinConditionSO
{
    public int turnsNumber;
    public override bool CheckForVictory()
    {
        if(GameManager.Instance.turnNumber == turnsNumber)
        {
            return true;
        }
        return false;
    }

    public override int ProgressAmount()
    {
        return GameManager.Instance.turnNumber;
    }

    public override int ProgressTarget()
    {
        return turnsNumber;
    }
}
