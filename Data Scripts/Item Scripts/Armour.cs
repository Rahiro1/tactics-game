using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Item, IStatsGiver
{
    public Define.ArmourType allowedArmourUser; // TODO- rework and delete this

    public int ArmourValue { get; private set; }
    public int BonusMaxHP { get; private set; }
    public int BonusStrength { get; private set; }
    public int BonusMagic { get; private set; }
    public int BonusOffence { get; private set; }
    public int BonusDefence { get; private set; }
    public int BonusResistance { get; private set; }
    public int BonusSpeed { get; private set; }
    public int BonusMove { get; private set; }

    public int BonusAttack { get; private set; }
    public int BonusOffensiveHit { get; private set; }
    public int BonusDefensiveHit { get; private set; }
    public int BonusAvoid { get; private set; }
    public int BonusCriticalRate { get; private set; }
    public int BonusCriticalAvod { get; private set; }
    public int BonusGuard { get; private set; }
    public int BonusWield { get; private set; }
    public int BonusRending { get; private set; }
    public int BonusRange { get; private set; }


    public Define.WeaponType weaponType { get; private set; }
    public int weaponRank { get; private set; }
    public int weaponMasteryLevel { get; private set; }
    public Define.WeaponType secondaryWeaponType { get; private set; }
    public int secondaryWeaponRank { get; private set; }
    public int secondaryWeaponMasteryLevel { get; private set; }
    public Define.WeaponType tertiaryWeaponType { get; private set; }
    public int tertiaryWeaponRank { get; private set; }
    public int tertiaryWeaponMasteryLevel { get; private set; }

    public Armour(ArmourSO template) : base(template)
    {
        allowedArmourUser = template.allowedArmourUser;
        ArmourValue = template.armourValue;
        BonusOffence = template.armourOffence;
        BonusDefence = template.armourDefence;

        BonusStrength = template.bonusStrength;
        BonusMagic = template.bonusMagic;
        BonusSpeed = template.bonusSpeed;
        BonusResistance = template.bonusResistance;

        weaponType = template.weaponType;
        weaponRank = template.weaponRank;
        weaponMasteryLevel = template.weaponMasteryLevel;
        secondaryWeaponType = template.secondaryWeaponType;
        secondaryWeaponRank = template.secondaryWeaponRank;
        secondaryWeaponMasteryLevel = template.secondaryWeaponMasteryLevel;
        tertiaryWeaponType = template.tertiaryWeaponType;
        tertiaryWeaponRank = template.tertiaryWeaponRank;
        tertiaryWeaponMasteryLevel = template.tertiaryWeaponMasteryLevel;
    }

    // TODO- complete these
    public string EquipmentName()
    {
        return ItemName;
    }

    public int MaxHPModifier()
    {
        return BonusMaxHP;
    }

    public int StrengthModifier()
    {
        return BonusStrength;
    }

    public int MagicModifier()
    {
        return BonusMagic;
    }

    public int OffenceModifier()
    {
        return BonusOffence;
    }

    public int DefenceModifier()
    {
        return BonusDefence;
    }

    public int ResistanceModifier()
    {
        return BonusResistance;
    }

    public int SpeedModifier()
    {
        return BonusSpeed;
    }

    public int MoveModifier()
    {
        return BonusMove;
    }

    public int MaxArmourModifier()
    {
        return ArmourValue;
    }

    public int AttackModifier()
    {
        return BonusAttack;
    }

    public int OffensiveHitModifier()
    {
        return BonusOffensiveHit;
    }

    public int DefensiveHitModifier()
    {
        return BonusDefensiveHit;
    }

    public int AvoidModifier()
    {
        return BonusAvoid;
    }

    public int CriticalRateModifier()
    {
        return BonusCriticalRate;
    }

    public int CriticalAvoidModifier()
    {
        return BonusCriticalAvod;
    }

    public int GuardModifier()
    {
        return BonusGuard;
    }

    public int WieldModifier()
    {
        return BonusWield;
    }

    public int RendingModifier()
    {
        return BonusRending;
    }

    public int RangeModifier()
    {
        return BonusRange;
    }

    [JsonConstructor]
    public Armour(int armourValue, int bonusMaxHP, int bonusStrength, int bonusMagic, int bonusOffence, int bonusDefence, int bonusResistance, int bonusSpeed,
        int bonusMove, int bonusAttack, int bonusOffensiveHit, int bonusDefensiveHit, int bonusAvoid, int bonusCriticalRate, int bonusCriticalAvod, int bonusGuard,
        int bonusWield, int bonusRending, int bonusRange, Define.WeaponType weaponType, int weaponRank, int weaponMasteryLevel, Define.WeaponType secondaryWeaponType,
        int secondaryWeaponRank, int secondaryWeaponMasteryLevel, Define.WeaponType tertiaryWeaponType, int tertiaryWeaponRank, int tertiaryWeaponMasteryLevel, int itemID,
        string itemName, string itemDescription, int itemBaseCost, int itemCurrentDurability, int itemMaxDurability, bool isUseable, bool isUnbreakable, List<int> skillIDList,
        List<SkillSO> skillList) : base(itemID, itemName, itemDescription, itemBaseCost, itemCurrentDurability, itemMaxDurability, isUseable, isUnbreakable, skillIDList, skillList)
    {
        ArmourValue = armourValue;
        BonusMaxHP = bonusMaxHP;
        BonusStrength = bonusStrength;
        BonusMagic = bonusMagic;
        BonusOffence = bonusOffence;
        BonusDefence = bonusDefence;
        BonusResistance = bonusResistance;
        BonusSpeed = bonusSpeed;
        BonusMove = bonusMove;
        BonusAttack = bonusAttack;
        BonusOffensiveHit = bonusOffensiveHit;
        BonusDefensiveHit = bonusDefensiveHit;
        BonusAvoid = bonusAvoid;
        BonusCriticalRate = bonusCriticalRate;
        BonusCriticalAvod = bonusCriticalAvod;
        BonusGuard = bonusGuard;
        BonusWield = bonusWield;
        BonusRending = bonusRending;
        BonusRange = bonusRange;
        this.weaponType = weaponType;
        this.weaponRank = weaponRank;
        this.weaponMasteryLevel = weaponMasteryLevel;
        this.secondaryWeaponType = secondaryWeaponType;
        this.secondaryWeaponRank = secondaryWeaponRank;
        this.secondaryWeaponMasteryLevel = secondaryWeaponMasteryLevel;
        this.tertiaryWeaponType = tertiaryWeaponType;
        this.tertiaryWeaponRank = tertiaryWeaponRank;
        this.tertiaryWeaponMasteryLevel = tertiaryWeaponMasteryLevel;
    }

}
