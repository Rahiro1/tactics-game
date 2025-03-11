using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{

    public LevelCounter Level { get; private set; } 
    // base stats
    public Stat HP { get; private set; }
    public Stat Strength { get; private set; }
    public Stat Magic { get; private set; }
    public Stat Offence { get; private set; }
    public Stat Defence { get; private set; }
    public Stat Resistance { get; private set; }
    public Stat Speed { get; private set; }
    public Stat Move { get; private set; }
    // weapon ranks
    public List<LevelCounter> WeaponRanks { get; private set; }
    public LevelCounter SwordRank { get; private set; }
    public LevelCounter SpearRank { get; private set; }
    public LevelCounter AxeRank { get; private set; }
    public LevelCounter BowRank { get; private set; }
    public LevelCounter ElementalRank { get; private set; }
    public LevelCounter DecayRank { get; private set; }
    public LevelCounter HealRank { get; private set; }
    public LevelCounter ArmourWeaponRank { get; private set; }
    public LevelCounter CreationRank { get; private set; }
    // derived stat bonuses
    public Stat BonusMaxArmour { get; private set; }
    public Stat BonusAttack { get; private set; }
    public Stat BonusOffensiveHit { get; private set; }
    public Stat BonusDefensiveHit { get; private set; }
    public Stat BonusAvoid { get; private set; }
    public Stat BonusCriticalRate { get; private set; }
    public Stat BonusCriticalAvoid { get; private set; }
    public Stat BonusGuard { get; private set; }
    public Stat BonusWield { get; private set; }
    public Stat BonusRending { get; private set; }
    public Stat BonusRange { get; private set; }

    public CharacterStats(CharacterSO character)
    {
        ClassSO startingClass = character.baseClass;

        Level = new LevelCounter(character.level, 1, Define.WeaponType.none);

        // starting stats
        int hpTemp = character.baseHP + startingClass.baseHP;
        int str = character.baseStrength + startingClass.baseStrength;
        int mag = character.baseMagic + startingClass.baseMagic;
        int off = character.baseOffence + startingClass.baseOffence;
        int def = character.baseDefence + startingClass.baseDefence;
        int res = character.baseResistance + startingClass.baseResistance;
        int spd = character.baseSpeed + startingClass.baseSpeed;
        int mov = character.baseMove + startingClass.baseMove;

        // calculate growths
        int hpGrowth = character.growthHP + startingClass.growthHP;
        int StrengthGrowth = character.growthStrength + startingClass.growthStrength;
        int MagicGrowth = character.growthMagic + startingClass.growthMagic;
        int OffenceGrowth = character.growthOffence + startingClass.growthOffence;
        int DefenceGrowth = character.growthDefence + startingClass.growthDefence;
        int ResistanceGrowth = character.growthResistance + startingClass.growthResistance;
        int SpeedGrowth = character.growthSpeed + startingClass.growthSpeed;
        int MoveGrowth = 0;

        // calculate max stats
        int MaximumHP = character.maxHP + startingClass.maxHP;
        int MaxStrength = character.maxStrength + startingClass.maxStrength;
        int MaxMagic = character.maxMagic + startingClass.maxMagic;
        int MaxOffence = character.maxOffence + startingClass.maxOffence;
        int MaxDefence = character.maxDefence + startingClass.maxDefence;
        int MaxResistance = character.maxResistance + startingClass.maxResistance;
        int MaxSpeed = character.maxSpeed + startingClass.maxSpeed;
        int MaxMove = character.maxMove + startingClass.maxMove;

        // assign to unmodified stats
        HP = new Stat(hpTemp, hpGrowth, MaximumHP);
        Strength = new Stat(str, StrengthGrowth, MaxStrength);
        Magic = new Stat(mag, MagicGrowth, MaxMagic);
        Offence = new Stat(off, OffenceGrowth, MaxOffence);
        Defence = new Stat(def, DefenceGrowth, MaxDefence);
        Resistance = new Stat(res, ResistanceGrowth, MaxResistance);
        Speed = new Stat(spd, SpeedGrowth, MaxSpeed);
        Move = new Stat(mov, MoveGrowth, MaxMove);

        // create empty bonus stats
        BonusMaxArmour = new Stat(0,0,0);
        BonusAttack = new Stat(0, 0, 0);
        BonusOffensiveHit = new Stat(0, 0, 0);
        BonusDefensiveHit = new Stat(0, 0, 0);
        BonusAvoid = new Stat(0, 0, 0);
        BonusCriticalRate = new Stat(0, 0, 0);
        BonusCriticalAvoid = new Stat(0, 0, 0);
        BonusGuard= new Stat(0, 0, 0);
        BonusWield = new Stat(0, 0, 0);
        BonusRending = new Stat(0, 0, 0);
        BonusRange = new Stat(0, 0, 0);


        // assign weapon ranks
        SwordRank = new LevelCounter(character.swordRank, character.swordMastery, Define.WeaponType.Sword);
        SpearRank = new LevelCounter(character.spearRank, character.spearMastery, Define.WeaponType.Polearm);
        AxeRank = new LevelCounter(character.axeRank, character.axeMastery, Define.WeaponType.Axe);
        BowRank = new LevelCounter(character.bowRank, character.bowMastery, Define.WeaponType.Bow);
        ElementalRank = new LevelCounter(character.elementalRank, character.elementalMastery, Define.WeaponType.Elemental);
        DecayRank = new LevelCounter(character.decayRank, character.bowMastery, Define.WeaponType.Decay);
        HealRank = new LevelCounter(character.healRank, character.healMastery, Define.WeaponType.Healing);
        ArmourWeaponRank = new LevelCounter(character.armourWeaponRank, character.armourWeaponMastery, Define.WeaponType.Armour);

        // add to weaponranks list
        WeaponRanks = new List<LevelCounter>();
        WeaponRanks.Add(SwordRank);
        WeaponRanks.Add(SpearRank);
        WeaponRanks.Add(AxeRank);
        WeaponRanks.Add(BowRank);
        WeaponRanks.Add(ElementalRank);
        WeaponRanks.Add(DecayRank);
        WeaponRanks.Add(HealRank);
        WeaponRanks.Add(ArmourWeaponRank);

        CreationRank = new LevelCounter(character.creationRank, character.creationMastery, Define.WeaponType.Creation); // TODO - implement creation rank somewhere? 
    }

    public CharacterStats(Define.GenericEnemyData unitData)
    {
        ClassSO classSO = unitData.unitClass;

        Level = new LevelCounter(unitData.level, 1, Define.WeaponType.none);

        // TODO add difficulty options and static options class, bool IsTough, bool IsSuperTough, bool IsStrong, bool IsSuperStrong, modifying the below ints
        int hPDifficultyModifier = 0;
        int strengthDifficultyModifier = 0;
        int magicDifficultyModifier = 0;
        int offenceDifficultyModifier = 0;
        int defenceDifficultyModifier = 0;
        int resistanceDifficultyModifier = 0;
        int speedDifficultyModifier = 0;

        // calculate growths

        int hPGrowth = classSO.growthHP + hPDifficultyModifier;
        int StrengthGrowth = classSO.growthStrength + strengthDifficultyModifier;
        int MagicGrowth = classSO.growthMagic + magicDifficultyModifier;
        int OffenceGrowth = classSO.growthOffence + offenceDifficultyModifier;
        int DefenceGrowth = classSO.growthDefence + defenceDifficultyModifier;
        int ResistanceGrowth = classSO.growthResistance + resistanceDifficultyModifier;
        int SpeedGrowth = classSO.growthSpeed + speedDifficultyModifier;
        int MoveGrowth = 0;

        int MaximumHP = classSO.maxHP;
        int MaxStrength = classSO.maxStrength;
        int MaxMagic = classSO.maxMagic;
        int MaxOffence = classSO.maxOffence;
        int MaxDefence = classSO.maxDefence;
        int MaxResistance = classSO.maxResistance;
        int MaxSpeed = classSO.maxSpeed;
        int MaxMove = classSO.maxMove;

        // calculax stats
        int hpTemp = Mathf.Min(classSO.maxHP, Mathf.RoundToInt(classSO.baseHP + (Level.Level - 1) * hPGrowth / 100f));
        int str = Mathf.Min(classSO.maxStrength, Mathf.RoundToInt(classSO.baseStrength + (Level.Level - 1) * StrengthGrowth / 100f));
        int mag = Mathf.Min(classSO.maxMagic, Mathf.RoundToInt(classSO.baseMagic + (Level.Level - 1) * MagicGrowth / 100f));
        int off = Mathf.Min(classSO.maxOffence, Mathf.RoundToInt(classSO.baseOffence + (Level.Level - 1) * OffenceGrowth / 100f));
        int def = Mathf.Min(classSO.maxDefence, Mathf.RoundToInt(classSO.baseDefence + (Level.Level - 1) * DefenceGrowth / 100f));
        int res = Mathf.Min(classSO.maxResistance, Mathf.RoundToInt(classSO.baseResistance + (Level.Level - 1) * ResistanceGrowth / 100f));
        int spd = Mathf.Min(classSO.maxSpeed, Mathf.RoundToInt(classSO.baseSpeed + (Level.Level - 1) * SpeedGrowth / 100f));
        int mov = classSO.baseMove;

        // assign unmodified stats
        HP = new Stat(hpTemp, hPGrowth, MaximumHP);
        Strength = new Stat(str, StrengthGrowth, MaxStrength);
        Magic = new Stat(mag, MagicGrowth, MaxMagic);
        Offence = new Stat(off, OffenceGrowth, MaxOffence);
        Defence = new Stat(def, DefenceGrowth, MaxDefence);
        Resistance = new Stat(res, ResistanceGrowth, MaxResistance);
        Speed = new Stat(spd, SpeedGrowth, MaxSpeed);
        Move = new Stat(mov, MoveGrowth, MaxMove);

        // create empty bonus stats
        BonusMaxArmour = new Stat(0, 0, 0);
        BonusAttack = new Stat(0, 0, 0);
        BonusOffensiveHit = new Stat(0, 0, 0);
        BonusDefensiveHit = new Stat(0, 0, 0);
        BonusAvoid = new Stat(0, 0, 0);
        BonusCriticalRate = new Stat(0, 0, 0);
        BonusCriticalAvoid = new Stat(0, 0, 0);
        BonusGuard = new Stat(0, 0, 0);
        BonusWield = new Stat(0, 0, 0);
        BonusRending = new Stat(0, 0, 0);
        BonusRange = new Stat(0, 0, 0);

        int weaponScaling = Mathf.FloorToInt(Level.Level / 2);

        SwordRank = new LevelCounter(classSO.swordRank + weaponScaling, classSO.swordMastery, Define.WeaponType.Sword); // CONSIDER - add a dificulty option for enemy weapon ranks and implement here
        SpearRank = new LevelCounter(classSO.spearRank + weaponScaling, classSO.spearMastery, Define.WeaponType.Polearm);
        AxeRank = new LevelCounter(classSO.axeRank + weaponScaling, classSO.axeMastery, Define.WeaponType.Axe);
        BowRank = new LevelCounter(classSO.bowRank + weaponScaling, classSO.bowMastery, Define.WeaponType.Bow);
        ElementalRank = new LevelCounter(classSO.elementalRank + weaponScaling, classSO.elementalMastery, Define.WeaponType.Elemental);
        DecayRank = new LevelCounter(classSO.decayRank + weaponScaling, classSO.decayMastery, Define.WeaponType.Decay);
        HealRank = new LevelCounter(classSO.healRank + weaponScaling, classSO.healMastery, Define.WeaponType.Healing);
        ArmourWeaponRank = new LevelCounter(classSO.armourWeaponRank + weaponScaling, classSO.armourWeaponMastery, Define.WeaponType.Armour);
        CreationRank = new LevelCounter(classSO.creationRank + weaponScaling, classSO.creationMastery, Define.WeaponType.Creation);

        // add to weaponranks list 
        WeaponRanks = new List<LevelCounter>();
        WeaponRanks.Add(SwordRank);
        WeaponRanks.Add(SpearRank);
        WeaponRanks.Add(AxeRank);
        WeaponRanks.Add(BowRank);
        WeaponRanks.Add(ElementalRank);
        WeaponRanks.Add(DecayRank);
        WeaponRanks.Add(HealRank);
        WeaponRanks.Add(ArmourWeaponRank);
        // TODO - add creation?
    }

    public void ClassChange(ClassSO oldClass, ClassSO newClass)
    {
        // base stat difference
        int hpTemp = newClass.baseHP + oldClass.baseHP;
        int str = newClass.baseStrength + oldClass.baseStrength;
        int mag = newClass.baseMagic + oldClass.baseMagic;
        int off = newClass.baseOffence + oldClass.baseOffence;
        int def = newClass.baseDefence + oldClass.baseDefence;
        int res = newClass.baseResistance + oldClass.baseResistance;
        int spd = newClass.baseSpeed + oldClass.baseSpeed;
        int mov = newClass.baseMove + oldClass.baseMove;

        HP.ChangeBaseStatValue(hpTemp);
        Strength.ChangeBaseStatValue(str);
        Magic.ChangeBaseStatValue(mag);
        Offence.ChangeBaseStatValue(off);
        Defence.ChangeBaseStatValue(def);
        Resistance.ChangeBaseStatValue(res);
        Speed.ChangeBaseStatValue(spd);
        Move.ChangeBaseStatValue(mov);

        // calculate growth difference
        int hpGrowth = newClass.growthHP + oldClass.growthHP;
        int strengthGrowth = newClass.growthStrength + oldClass.growthStrength;
        int magicGrowth = newClass.growthMagic + oldClass.growthMagic;
        int offenceGrowth = newClass.growthOffence + oldClass.growthOffence;
        int defenceGrowth = newClass.growthDefence + oldClass.growthDefence;
        int resistanceGrowth = newClass.growthResistance + oldClass.growthResistance;
        int speedGrowth = newClass.growthSpeed + oldClass.growthSpeed;
        int moveGrowth = 0;

        HP.ChangeStatGrowth(hpGrowth);
        Strength.ChangeStatGrowth(strengthGrowth);
        Magic.ChangeStatGrowth(magicGrowth);
        Offence.ChangeStatGrowth(offenceGrowth);
        Defence.ChangeStatGrowth(defenceGrowth);
        Resistance.ChangeStatGrowth(resistanceGrowth);
        Speed.ChangeStatGrowth(speedGrowth);
        Move.ChangeStatGrowth(moveGrowth);

        // calculate max stat difference
        int maxMaxHP = newClass.maxHP + oldClass.maxHP;
        int maxStrength = newClass.maxStrength + oldClass.maxStrength;
        int maxMagic = newClass.maxMagic + oldClass.maxMagic;
        int maxOffence = newClass.maxOffence + oldClass.maxOffence;
        int maxDefence = newClass.maxDefence + oldClass.maxDefence;
        int maxResistance = newClass.maxResistance + oldClass.maxResistance;
        int maxSpeed = newClass.maxSpeed + oldClass.maxSpeed;
        int maxMove = newClass.maxMove + oldClass.maxMove;

        HP.ChangeStatMax(maxMaxHP);
        Strength.ChangeStatMax(maxStrength);
        Magic.ChangeStatMax(maxMagic);
        Offence.ChangeStatMax(maxOffence);
        Defence.ChangeStatMax(maxDefence);
        Resistance.ChangeStatMax(maxResistance);
        Speed.ChangeStatMax(maxSpeed);
        Move.ChangeStatMax(maxMove);
    }

    public void AddModifiersFromComplexEquippable(IComplexStatsGiver equip, Character character)
    {
        string sourceName = equip.EquipmentName();

        HP.AddModifier(sourceName, equip.MaxHPModifier(character));
        Strength.AddModifier(sourceName, equip.StrengthModifier(character));
        Magic.AddModifier(sourceName, equip.MagicModifier(character));
        Offence.AddModifier(sourceName, equip.OffenceModifier(character));
        Defence.AddModifier(sourceName, equip.DefenceModifier(character));
        Resistance.AddModifier(sourceName, equip.ResistanceModifier(character));
        Speed.AddModifier(sourceName, equip.SpeedModifier(character));
        Move.AddModifier(sourceName, equip.MoveModifier(character));
        BonusMaxArmour.AddModifier(sourceName, equip.MaxArmourModifier(character));
        BonusAttack.AddModifier(sourceName, equip.AttackModifier(character));
        BonusOffensiveHit.AddModifier(sourceName, equip.OffensiveHitModifier(character));
        BonusDefensiveHit.AddModifier(sourceName, equip.DefensiveHitModifier(character));
        BonusAvoid.AddModifier(sourceName, equip.AvoidModifier(character));
        BonusCriticalRate.AddModifier(sourceName, equip.CriticalRateModifier(character));
        BonusCriticalAvoid.AddModifier(sourceName, equip.CriticalAvoidModifier(character));
        BonusGuard.AddModifier(sourceName, equip.GuardModifier(character));
        BonusWield.AddModifier(sourceName, equip.WieldModifier(character));
        BonusRending.AddModifier(sourceName, equip.RendingModifier(character));
        BonusRange.AddModifier(sourceName, equip.RangeModifier(character));
    }

    public void AddModifiersFromEquippable(IStatsGiver equip)
    {
        string sourceName = equip.EquipmentName();

        
        HP.AddModifier(sourceName, equip.MaxHPModifier());
        Strength.AddModifier(sourceName, equip.StrengthModifier());
        Magic.AddModifier(sourceName, equip.MagicModifier());
        Offence.AddModifier(sourceName, equip.OffenceModifier());
        Defence.AddModifier(sourceName, equip.DefenceModifier());
        Resistance.AddModifier(sourceName, equip.ResistanceModifier());
        Speed.AddModifier(sourceName, equip.SpeedModifier());
        Move.AddModifier(sourceName, equip.MoveModifier());
        BonusMaxArmour.AddModifier(sourceName, equip.MaxArmourModifier());
        BonusAttack.AddModifier(sourceName, equip.AttackModifier());
        BonusOffensiveHit.AddModifier(sourceName, equip.OffensiveHitModifier());
        BonusDefensiveHit.AddModifier(sourceName, equip.DefensiveHitModifier());
        BonusAvoid.AddModifier(sourceName, equip.AvoidModifier());
        BonusCriticalRate.AddModifier(sourceName, equip.CriticalRateModifier());
        BonusCriticalAvoid.AddModifier(sourceName, equip.CriticalAvoidModifier());
        BonusGuard.AddModifier(sourceName, equip.GuardModifier());
        BonusWield.AddModifier(sourceName, equip.WieldModifier());
        BonusRending.AddModifier(sourceName, equip.RendingModifier());
        BonusRange.AddModifier(sourceName, equip.RangeModifier());
    }

    public void RemoveModifiers(string modifierName)
    {
        HP.RemoveModifier(modifierName);
        Strength.RemoveModifier(modifierName);
        Magic.RemoveModifier(modifierName);
        Offence.RemoveModifier(modifierName);
        Defence.RemoveModifier(modifierName);
        Resistance.RemoveModifier(modifierName);
        Speed.RemoveModifier(modifierName);
        Move.RemoveModifier(modifierName);
        BonusMaxArmour.RemoveModifier(modifierName);
        BonusAttack.RemoveModifier(modifierName);
        BonusOffensiveHit.RemoveModifier(modifierName);
        BonusDefensiveHit.RemoveModifier(modifierName);
        BonusAvoid.RemoveModifier(modifierName);
        BonusCriticalRate.RemoveModifier(modifierName);
        BonusCriticalAvoid.RemoveModifier(modifierName);
        BonusGuard.RemoveModifier(modifierName);
        BonusWield.RemoveModifier(modifierName);
        BonusRending.RemoveModifier(modifierName);
        BonusRange.RemoveModifier(modifierName);
    }

    [JsonConstructor]
    public CharacterStats(LevelCounter level, Stat hP, Stat strength, Stat magic, Stat offence, Stat defence, Stat resistance, Stat speed, Stat move, List<LevelCounter> weaponRanks,
        LevelCounter swordRank, LevelCounter spearRank, LevelCounter axeRank, LevelCounter bowRank, LevelCounter elementalRank, LevelCounter decayRank, LevelCounter healRank,
        LevelCounter armourWeaponRank, LevelCounter creationRank, Stat bonusMaxArmour, Stat bonusAttack, Stat bonusOffensiveHit, Stat bonusDefensiveHit, Stat bonusAvoid,
        Stat bonusCriticalRate, Stat bonusCriticalAvoid, Stat bonusGuard, Stat bonusWield, Stat bonusRending, Stat bonusRange)
    {
        Level = level;
        HP = hP;
        Strength = strength;
        Magic = magic;
        Offence = offence;
        Defence = defence;
        Resistance = resistance;
        Speed = speed;
        Move = move;
        WeaponRanks = weaponRanks;
        SwordRank = swordRank;
        SpearRank = spearRank;
        AxeRank = axeRank;
        BowRank = bowRank;
        ElementalRank = elementalRank;
        DecayRank = decayRank;
        HealRank = healRank;
        ArmourWeaponRank = armourWeaponRank;
        CreationRank = creationRank;
        BonusMaxArmour = bonusMaxArmour;
        BonusAttack = bonusAttack;
        BonusOffensiveHit = bonusOffensiveHit;
        BonusDefensiveHit = bonusDefensiveHit;
        BonusAvoid = bonusAvoid;
        BonusCriticalRate = bonusCriticalRate;
        BonusCriticalAvoid = bonusCriticalAvoid;
        BonusGuard = bonusGuard;
        BonusWield = bonusWield;
        BonusRending = bonusRending;
        BonusRange = bonusRange;
    }

}
