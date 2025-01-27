using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/LevelEnd/Bonus/Turn Bonus")]
public class BonusContitionTime : BonusConditionSO  // should be BonusConditioniTurns
{
    public int turnsForMax;
    public int turnsForMin;

    public override int BonusTimeCurrent()
    {
        if (ProgressAmount() < turnsForMax)
        {
            return bonusTime; // replace with function?
        }
        else if (ProgressAmount() > turnsForMin)
        {
            return 0;
        }
        else
        {
            if (turnsForMin - turnsForMax != 0)
            {
                float rewardProportion = (ProgressAmount() - turnsForMax) / (turnsForMin - turnsForMax);
                return Mathf.FloorToInt(bonusTime * rewardProportion);

            }
            else
            {
                return bonusTime;
            }
        }
    }

    public override bool CheckForVictory()
    {
        return true;
    }

    public override void GainOtherRewards()
    {
        // no other reward so this is empty
    }

    public override int ProgressAmount()
    {
        return GameManager.Instance.turnNumber;
    }

    public override int ProgressTarget()
    {
        return turnsForMax;
    }
}
