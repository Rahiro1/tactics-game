using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/LevelEnd/Victory/Rout")]
public class WinConditionRout : WinConditionSO
{
    public override bool CheckForVictory()
    {
        if(GameManager.Instance.enemyList.Count == 0)
        {
            return true;
        }
         
        return false;
    }

    public override int ProgressAmount()
    {
        return GameManager.Instance.enemyList.Count;
    }

    public override int ProgressTarget()
    {
        return -1;
    }
}
 