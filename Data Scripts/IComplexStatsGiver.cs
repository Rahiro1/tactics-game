using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComplexStatsGiver
{
    string EquipmentName();

    int MaxHPModifier(Character character);
    int StrengthModifier(Character character);
    int MagicModifier(Character character);
    int OffenceModifier(Character character);
    int DefenceModifier(Character character);
    int ResistanceModifier(Character character);
    int SpeedModifier(Character character);
    int MoveModifier(Character character);
    int MaxArmourModifier(Character character);
    int AttackModifier(Character character);
    int OffensiveHitModifier(Character character);
    int DefensiveHitModifier(Character character);
    int AvoidModifier(Character character);
    int CriticalRateModifier(Character character);
    int CriticalAvoidModifier(Character character);
    int GuardModifier(Character character);
    int WieldModifier(Character character);
    int RendingModifier(Character character);
    int RangeModifier(Character character);
}
