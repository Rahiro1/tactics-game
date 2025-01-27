using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter 
{
    // TODO - Decide on and implement max level
    public int Level { get; set; }
    public int Experience { get;  set; }
    public int MasteryLevel { get; set; }

    public Define.WeaponType weaponType; // .none for character level

    public LevelCounter()
    {

    }
    public LevelCounter(int startingLevel, int startingMastery, Define.WeaponType weaponType)
    {
        Level = startingLevel;
        MasteryLevel = startingMastery;
        this.weaponType = weaponType;
        Experience = 0;
    }

    public bool GainExperience(int amount)
    {
        int adjustedAmount = Mathf.Clamp(amount, 0, 100);

        if (Experience + adjustedAmount >= 100)
        {
            Experience += adjustedAmount - 100;
            Level++;
            return true;
        }
        else
        {
            Experience += adjustedAmount;
            return false;
        }
    }

    public int GetLevelTimesMastery()
    {
        return Level * MasteryLevel;
    }
}
