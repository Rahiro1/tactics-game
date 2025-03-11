using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Skills/Generic")]
public class SkillSO : ScriptableObject, IComplexStatsGiver
{
    public int skillID;
    public string skillName;
    public string description;
    public Sprite skillIcon;
    public Define.SkillTriggerType activationCondition;
    public Define.SkillTriggerType deactivationCondition;
    private bool isActive;
    public float RegenerationPercent;
    public float ArmourRegenerationPercent;
    public int RegenerationFlat;
    public int ArmourRegenerationFlat;

    public virtual bool UpdateActivityAndIsChanged(Define.SkillTriggerType skillTriggerType)
    {
        if (skillTriggerType == activationCondition)
        {
            isActive = true;
            return true;
        } else if(skillTriggerType == deactivationCondition)
        {
            isActive = false;
            return true;
        }
        return false;
    }

    public virtual bool IsActive(Character thisCharacter)
    {
        return isActive;
    }

    public string EquipmentName()
    {
        return skillName;
    }
    public virtual int MaxHPModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int StrengthModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int MagicModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int OffenceModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int DefenceModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int ResistanceModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int SpeedModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int MoveModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int MaxArmourModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int AttackModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int OffensiveHitModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int DefensiveHitModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int AvoidModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int CriticalRateModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int CriticalAvoidModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int GuardModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int WieldModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int RendingModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int RangeModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int RegenerationAmount(Character thisCharacter)
    {
        return RegenerationFlat + Mathf.CeilToInt(thisCharacter.MaxHP.GetbaseValue() * RegenerationPercent);
    }
    public virtual int ArmourRegenerationAmount(Character thisCharacter)
    {
        return ArmourRegenerationFlat + Mathf.CeilToInt(thisCharacter.MaxHP.GetbaseValue() * ArmourRegenerationPercent);
    }

    public virtual int OffensiveSkillActivationBonus(int damage, int rending, int skillroll, out int newRending)
    {
        newRending = rending;
        return damage;
    }

}
