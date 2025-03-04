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
        return StatReductionAmount(thisCharacter.Defence.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());
    }

    public override int MagicModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Magic.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());
    }

    public override int MoveModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Move.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());
    }

    public override int OffenceModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Offence.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());
    }

    public override int ResistanceModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Resistance.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());
    }

    public override int SpeedModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Speed.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());
    }

    public override int StrengthModifier(Character thisCharacter)
    {
        return StatReductionAmount(thisCharacter.Strength.GetbaseValue(), thisCharacter.currentHP, thisCharacter.HP.GetbaseValue());  // should this be base str?
    }

    public override int RegenerationAmount(Character thisCharacter)
    {
        return Mathf.CeilToInt(thisCharacter.HP.GetbaseValue() * regeneration / 100f);
    }

    public int StatReductionAmount(int statToModify, int currentHP, int maxHP)
    {
        return Mathf.FloorToInt(statToModify * (currentHP - maxHP) * statReductionWithHP / maxHP); // min covering up problem
    }
}
