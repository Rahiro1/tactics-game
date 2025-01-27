using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// there is no case where a skill object would be different from it's skill so so these seem unneccessary
public class Skill
{
    public SkillSO SkillSO { get; private set; }


    public bool IsActive(Character thisCharacter)
    {
        return SkillSO.IsActive(thisCharacter);
    }

    public int StrengthModifier(Character thisCharacter)
    {
        return SkillSO.StrengthModifier(thisCharacter);
    }

    public int MagicModifier(Character thisCharacter)
    {
        return SkillSO.MagicModifier(thisCharacter);
    }

    public int OffenceModifier(Character thisCharacter)
    {
        return SkillSO.OffenceModifier(thisCharacter);
    }

    public int DefenceModifier(Character thisCharacter)
    {
        return SkillSO.DefenceModifier(thisCharacter);
    }

    public int ResistanceModifier(Character thisCharacter)
    {
        return SkillSO.ResistanceModifier(thisCharacter);
    }

    public int SpeedModifier(Character thisCharacter)
    {
        return SkillSO.SpeedModifier(thisCharacter);
    }

    public int ArmourModifier(Character thisCharacter)
    {
        return SkillSO.ArmourModifier(thisCharacter);
    }

    public int MoveModifier(Character thisCharacter)
    {
        return SkillSO.MoveModifier(thisCharacter);
    }

    public int RegenerationAmount(Character thisCharacter)
    {
        return SkillSO.RegenerationAmount(thisCharacter);
    }
}
