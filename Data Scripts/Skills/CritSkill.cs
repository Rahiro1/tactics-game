using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Skills/Critical")]
public class CritSkill : SkillSO
{
    public int skillDamageFlat;
    public float skillDamagePercent = 1;
    public int skillRendingFlat;
    public float skillRendingPercent = 1;
    public int critDamageFlat;
    public float critDamagePercent = 2;
    public int critRendingFlat;
    public float critRendingPercent = 1;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="rending"></param>
    /// <param name="skillroll"> crit - crit avoid clamped between 0 and 200, determines likelihood of activation </param>
    /// <param name="newRending"></param>
    /// <returns></returns>
    public override int OffensiveSkillActivationBonus(int damage, int rending, int skillThreshold, out int newRending)
    {
        int rng = UnityEngine.Random.Range(0, 100);
        int newDamage = damage;
        newRending = rending;

        if(rng < skillThreshold)
        {
            newDamage += skillDamageFlat;
            newDamage = Mathf.FloorToInt(newDamage*skillDamagePercent);
            newRending += skillRendingFlat;
            newRending = Mathf.FloorToInt(newRending * skillRendingPercent);
            GameManager.Instance.skillNotificationDisplay.AddNotification(skillName);
        }
        if(rng < skillThreshold - 100)
        {
            newDamage += critDamageFlat;
            newDamage = Mathf.FloorToInt(newDamage*critDamagePercent);
            newRending += critRendingFlat;
            newRending = Mathf.FloorToInt(newRending * critRendingPercent);
            GameManager.Instance.skillNotificationDisplay.AddNotification("Critical"); // put this in Define? constant?
        }

        return newDamage;
    }
}
