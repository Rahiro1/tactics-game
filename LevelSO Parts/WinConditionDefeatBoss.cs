 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/LevelEnd/Victory/Defeat Boss")]
public class WinConditionDefeatBoss : WinConditionSO
{
    // class will require defeat of every boss with the same ID on the map. To return true if one of a set are killed, create two objects and give the bosses different IDs
    public int bossID;
    public override bool CheckForVictory()
    {
        foreach(UnitController enemy in GameManager.Instance.enemyList)
        {
            if(enemy.Character.characterID == bossID)       // check if any remaining enemy units have the same ID as the boss
            {
                return false;
            }
        }

        return true;
    }

    public override int ProgressAmount()
    {
        if (CheckForVictory())
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public override int ProgressTarget()
    {
        return 1;
    }
}
