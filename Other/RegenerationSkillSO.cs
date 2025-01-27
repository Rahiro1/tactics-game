using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Skills/Regeneration")]
public class RegenerationSkillSO : SkillSO
{
    public int regeneration;
    public float statReductionWithHP;

    public override int DefenceModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.UnmodifiedDefence.value, thisCharacter.currentHP, thisCharacter.HP.value);
    }

    public override bool IsActive(Character thisCharacter)
    {
        return true;
    }

    public override int MagicModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.UnmodifiedMagic.value, thisCharacter.currentHP, thisCharacter.HP.value);
    }

    public override int MoveModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Move.value, thisCharacter.currentHP, thisCharacter.HP.value);
    }

    public override int OffenceModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.UnmodifiedOffence.value, thisCharacter.currentHP, thisCharacter.HP.value);
    }

    public override int ResistanceModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.UnmodifiedResistance.value, thisCharacter.currentHP, thisCharacter.HP.value);
    }

    public override int SpeedModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.UnmodifiedSpeed.value, thisCharacter.currentHP, thisCharacter.HP.value);
    }

    public override int StrengthModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.UnmodifiedStrength.value, thisCharacter.currentHP, thisCharacter.HP.value);  // should this be base str?
    }

    public override int RegenerationAmount(Character thisCharacter)
    {
        return Mathf.CeilToInt(thisCharacter.HP.value * regeneration / 100f);
    }

    public int StatReductionAmount(int statToModify, int currentHP, int maxHP)
    {
        return Mathf.FloorToInt(statToModify * (currentHP - maxHP) * statReductionWithHP / maxHP); // min covering up problem
    }
}
