using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private int value;
    private int growth;
    private int max;
    private List<StatModifier> modifiers = new List<StatModifier>();

    public Stat(int statStartingValue,int statGrowth ,int statMax)
    {
        value = statStartingValue;
        growth = statGrowth;
        max = statMax; 
    }

    public int GetbaseValue()
    {
        return value;
    }

    public int GetModifiedValue()
    {
        int modifiedValue = value;
        foreach (StatModifier statModifier in modifiers)
        {
            modifiedValue += statModifier.modifier;
        }

        return modifiedValue;
    }

    public void ChangeBaseStatValue(int amountToAdd)
    {
        Mathf.Clamp(value += amountToAdd, 0, max);
    }

    public void ChangeStatGrowth(int amountToAdd)
    {
        growth += amountToAdd;
    }

    public void ChangeStatMax(int amountToAdd)
    {
        max += amountToAdd;
    }

    public List<StatModifier> GetModifiers()
    {
        return modifiers;
    }

    public void AddModifier(string source, int modifierValue)
    {
        RemoveModifier(source);

        if(modifierValue != 0)
        {
            modifiers.Add(new StatModifier(source, modifierValue));
        }    }

    public void RemoveModifier(string source)
    {
        for(int i = modifiers.Count -1; i>-1; i--)
        {
            if (modifiers[i].source == source)
            {
                modifiers.RemoveAt(i);
            }
        }
    }

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

    [JsonConstructor]
    public Stat(int value, int growth, int max, List<StatModifier> modifiers)
    {
        this.value = value;
        this.growth = growth;
        this.max = max;
        this.modifiers = modifiers;
    }
}
