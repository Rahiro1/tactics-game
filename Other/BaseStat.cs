using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public int value;
    public int growth;
    public int max;

    public BaseStat()
    {

    }

    public BaseStat(int statStartingValue,int statGrowth ,int statMax)
    {
        value = statStartingValue;
        growth = statGrowth;
        max = statMax; 
    }

    public void ChangeBaseStatValue(int amount)
    {
        Mathf.Clamp(value += amount, 0, max);
    }

    public void ChangeStatGrowth(int amount)
    {
        growth += amount;
    }

    public void ChangeStatMax(int amount)
    {
        max += amount;
    }

    /// <summary>
    /// increases the stat by a random amount determined by statGrowth
    /// </summary>
    /// <returns>
    /// the amount the stat increased by
    /// </returns>
    public int LevelUp()
    {
        int growthAmount = DetermineGrowthAmount();
        ChangeBaseStatValue(growthAmount);
        return growthAmount;
    }

    public int DetermineGrowthAmount()
    {
        int growthAmount = 0;
        int remainingGrowth = growth;
        while (remainingGrowth - 100 > 0)
        {
            growthAmount += 1;
            remainingGrowth -= 100;
        }
        if (Random.Range(1, 101) < growth)
        {
            growthAmount += 1;
        }

        return growthAmount;

    }
}
