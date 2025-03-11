using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, IStatsGiver
{
    //main weapon stats
    public bool IsMagical { get; private set; }
    public int Attack { get; private set; }
    public int Rending { get; private set; }
    public int CriticalRate { get; private set; }
    public int Offence { get; private set; }
    public int Defence { get; private set; }
    public int Range { get; private set; }

    // other stats
    public int BonusMaxHP { get; private set; }
    public int BonusStrength { get; private set; }
    public int BonusMagic { get; private set; }
    public int BonusOffence { get; private set; }
    public int BonusDefence { get; private set; }
    public int BonusResistance { get; private set; }
    public int BonusSpeed { get; private set; }
    public int BonusMove { get; private set; }
    public int BonusArmour { get; private set; }
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

    //weapon ranks
    public Define.WeaponType weaponType { get; private set; }
    public int weaponRank { get; private set; }
    public int weaponMasteryLevel { get; private set; }
    public Define.WeaponType secondaryWeaponType { get; private set; }
    public int secondaryWeaponRank { get; private set; }
    public int secondaryWeaponMasteryLevel { get; private set; }
    public Define.WeaponType tertiaryWeaponType { get; private set; }
    public int tertiaryWeaponRank { get; private set; }
    public int tertiaryWeaponMasteryLevel { get; private set; }


    public Weapon(WeaponSO template) : base(template)
    {
        IsMagical = template.IsMagical;
        Attack = template.power;
        //hitRate = template.hitRate;
        Rending = template.rending;
        CriticalRate = template.criticalRate;
        Offence = template.offence;
        Defence = template.defence;
        BonusRange = template.range;

        BonusStrength = template.bonusStrength;
        BonusMagic = template.bonusMagic;
        BonusSpeed = template.bonusSpeed;
        BonusArmour = template.bonusArmour;
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

    public bool CanEquip(Character character)
    {
        return true;
    }

    // TODO- complete these
    public string EquipmentName()
    {
        return ItemName;
    }

    public int MaxHPModifier()
    {
        return 0;
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
        return Offence;
    }

    public int DefenceModifier()
    {
        return Defence;
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
        return 0;
    }

    public int MaxArmourModifier()
    {
        return 0;
    }

    public int AttackModifier()
    {
        return Attack;
    }

    public int OffensiveHitModifier()
    {
        return 0;
    }

    public int DefensiveHitModifier()
    {
        return 0;
    }

    public int AvoidModifier()
    {
        return 0;
    }

    public int CriticalRateModifier()
    {
        return CriticalRate;
    }

    public int CriticalAvoidModifier()
    {
        return 0;
    }

    public int GuardModifier()
    {
        return 0;
    }

    public int WieldModifier()
    {
        return 0;
    }

    public int RendingModifier()
    {
        return Rending;
    }

    public int RangeModifier()
    {
        return BonusRange;
    }

    [JsonConstructor]
    public Weapon(bool isMagical, int attack, int rending, int criticalRate, int offence, int defence, int range, int bonusMaxHP, int bonusStrength, int bonusMagic,
        int bonusOffence, int bonusDefence, int bonusResistance, int bonusSpeed, int bonusMove, int bonusArmour, int bonusAttack, int bonusOffensiveHit, int bonusDefensiveHit,
        int bonusAvoid, int bonusCriticalRate, int bonusCriticalAvod, int bonusGuard, int bonusWield, int bonusRending, int bonusRange, Define.WeaponType weaponType, int weaponRank,
        int weaponMasteryLevel, Define.WeaponType secondaryWeaponType, int secondaryWeaponRank, int secondaryWeaponMasteryLevel, Define.WeaponType tertiaryWeaponType, int tertiaryWeaponRank,
        int tertiaryWeaponMasteryLevel, int itemID, string itemName, string itemDescription, int itemBaseCost, int itemCurrentDurability, int itemMaxDurability, bool isUseable, bool isUnbreakable,
        List<int> skillIDList, List<SkillSO> skillList) : base(itemID, itemName, itemDescription, itemBaseCost, itemCurrentDurability, itemMaxDurability, isUseable, isUnbreakable, skillIDList, skillList)
    {
        IsMagical = isMagical;
        Attack = attack;
        Rending = rending;
        CriticalRate = criticalRate;
        Offence = offence;
        Defence = defence;
        Range = range;
        BonusMaxHP = bonusMaxHP;
        BonusStrength = bonusStrength;
        BonusMagic = bonusMagic;
        BonusOffence = bonusOffence;
        BonusDefence = bonusDefence;
        BonusResistance = bonusResistance;
        BonusSpeed = bonusSpeed;
        BonusMove = bonusMove;
        BonusArmour = bonusArmour;
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

    // TODO consider forging system
}
