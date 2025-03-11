using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatsGiver
{
    string EquipmentName();

    int MaxHPModifier();
    int StrengthModifier();
    int MagicModifier();
    int OffenceModifier();
    int DefenceModifier();
    int ResistanceModifier();
    int SpeedModifier();
    int MoveModifier();
    int MaxArmourModifier();
    int AttackModifier();
    int OffensiveHitModifier();
    int DefensiveHitModifier();
    int AvoidModifier();
    int CriticalRateModifier();
    int CriticalAvoidModifier();
    int GuardModifier();
    int WieldModifier();
    int RendingModifier();
    int RangeModifier();
}
