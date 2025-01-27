using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Skills/Generic")]
public class SkillSO : ScriptableObject
{
    public int skillID;
    public string skillName;
    public string description;
    public Sprite skillIcon;
    public float RegenerationPercent;
    public float ArmourRegenerationPercent;
    public int RegenerationFlat;
    public int ArmourRegenerationFlat;

    public virtual bool IsActive(Character thisCharacter)
    {
        return true;
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

    public virtual int ArmourModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int MoveModifier(Character thisCharacter)
    {
        return 0;
    }

    public virtual int RegenerationAmount(Character thisCharacter)
    {
        return RegenerationFlat + Mathf.CeilToInt(thisCharacter.HP.value * RegenerationPercent);
    }
    public virtual int ArmourRegenerationAmount(Character thisCharacter)
    {
        return ArmourRegenerationFlat + Mathf.CeilToInt(thisCharacter.HP.value * ArmourRegenerationPercent);
    }

    public virtual int OffensiveSkillActivationBonus(int damage, int rending, int skillroll, out int newRending)
    {
        newRending = rending;
        return damage;
    }

}
