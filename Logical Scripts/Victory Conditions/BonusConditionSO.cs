using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusConditionSO : WinConditionSO
{
    public int bonusTime;
    public abstract void GainOtherRewards();
    public int BonusTimeMax()
    {
        return bonusTime;
    }
    public abstract int BonusTimeCurrent();
}
