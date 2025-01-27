using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character 
{
    public string characterName;
    public int characterID = -1;
    public int classID;
    public Define.UnitAllignment unitAllignment;
    public Define.AIType AIType;
    public Define.AIType secondaryAIType;
    public Define.UnitType unitType;
    public int battalionNumber;
    public LevelCounter Level { get; set; }

    // stats
    public BaseStat HP { get; set; }
    public int currentHP;
    public BaseStat UnmodifiedStrength { get; set; }
    public BaseStat UnmodifiedMagic { get; set; }
    public BaseStat UnmodifiedOffence { get; set; }
    public BaseStat UnmodifiedDefence { get; set; }
    public BaseStat UnmodifiedResistance { get; set; }
    public BaseStat UnmodifiedSpeed { get; set; }
    public BaseStat Move { get; set; }
    public int currentArmour;

    

    // stats after modifiers
    public int ModifiedStrength
    {
        get
        {
            int temp = UnmodifiedStrength.value;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.bonusStrength;
            }
            if (EquippedArmour != null)
            {
                temp += EquippedArmour.bonusStrength;
            }
            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.StrengthModifier(this);
                }
            }

            return temp;
        }
    }
    public int ModifiedMagic
    {
        get
        {
            int temp = UnmodifiedMagic.value;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.bonusMagic;
            }
            if (EquippedArmour != null)
            {
                temp += EquippedArmour.bonusMagic;
            }
            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.MagicModifier(this);
                }
            }

            return temp;
        }
    }
    public int ModifiedOffence
    {
        get
        {
            int temp = UnmodifiedOffence.value;
            if(EquippedWeapon != null)
            {
                temp += EquippedWeapon.offence;
            }
            if(EquippedArmour != null)
            {
                temp += EquippedArmour.armourOffence;
            }
            temp += Wield;

            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.OffenceModifier(this);
                }
            }

            return temp;
        }
    }
    public int ModifiedDefence
    {
        get
        {
            int temp = UnmodifiedDefence.value;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.defence;
            }
            if (EquippedArmour != null)
            {
                temp += EquippedArmour.armourDefence;
            }
            temp += Wield;
            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.DefenceModifier(this);
                }
            }

            return temp;
        }
    }
    public int ModifiedResistance
    {
        get
        {
            int temp = UnmodifiedResistance.value;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.bonusResistance;
            }
            if (EquippedArmour != null)
            {
                temp += EquippedArmour.bonusResistance;
            }
            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.ResistanceModifier(this);
                }
            }

            return temp;
        }
    }
    public int ModifiedSpeed
    {
        get
        {
            int temp = UnmodifiedSpeed.value;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.bonusSpeed;
            }
            if (EquippedArmour != null)
            {
                temp += EquippedArmour.bonusSpeed;
            }
            temp += Mathf.FloorToInt(Wield / 2);


            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.SpeedModifier(this);
                }
            }

            return temp;
        }
    }
    public int ModifiedArmour
    {
        get
        {
            int temp = 0;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.bonusArmour;
            }
            if (EquippedArmour != null)
            {
                temp += EquippedArmour.armourValue;
            }
            foreach (SkillSO skill in GetSkillList())
            {
                if (skill.IsActive(this))
                {
                    temp += skill.ArmourModifier(this);
                }
            }

            return temp;
        }
    }

    // derived Stats
    // TODO add skil modifiers
    // TODO add constants for multipliers?
    public int Attack
    {
        get
        {
            if (EquippedWeapon == null)
            {
                return 0; // check that this is correct 
            }
            else if (EquippedWeapon.IsMagical)
            {
                return ModifiedMagic + EquippedWeapon.power;
            }
            else
            {
                return ModifiedStrength + EquippedWeapon.power;
            }
        }
    }
    public int OffensiveHit
    {
        get
        {
            if (EquippedWeapon != null)
            {
                return ModifiedOffence * 4 + 80; //TODO review hitrate specifics
            }
            else
            {
                return 0;
            }
        }
    }
    public int DefensiveHit
    {
        get
        {
            if (EquippedWeapon != null)
            {
                return ModifiedDefence * 4 + 80;
            }
            else
            {
                return 0;
            }
        }
    }
    public int Avoid
    {
        get
        {
            return ModifiedSpeed * 3 + ModifiedDefence ;
        }
    }
    public int CriticalRate
    {
        get
        {
            int temp = 0;
            temp += Mathf.FloorToInt(ModifiedOffence / 2); 
            if(EquippedWeapon != null)
            {
                temp += EquippedWeapon.criticalRate;
            }
            // TODO - add skills that affect crit rate here
            return temp;
        }
    }
    public int CriticalAvoid
    {
        get
        {
            return Mathf.Max(ModifiedDefence,1);

        }
    }
    public int Guard
    {
        get
        {
            return Mathf.CeilToInt(ModifiedDefence / 2f) + currentArmour;
        }
    }

    public int Wield
    {
        // TODO - complete this, changing weapon ranks to make them simpler
        get
        {
            int temp = 0;
            int wLvl = 0;
            int armourTemp = 0; 

            if(EquippedWeapon != null)
            {
                temp -= EquippedWeapon.complexity;

                wLvl = SelectWeaponLevelType(EquippedWeapon.weaponType).Level;
                wLvl -= EquippedWeapon.weaponRank;

                if (wLvl >= 0)
                {
                    temp += wLvl;
                }
                else
                {
                    temp += wLvl * 2;
                }


                if(EquippedWeapon.secondaryWeaponType != Define.WeaponType.none)
                {
                    wLvl = SelectWeaponLevelType(EquippedWeapon.secondaryWeaponType).Level;
                    wLvl -= EquippedWeapon.secondaryWeaponRank;

                    if (wLvl >= 0)
                    {
                        temp += wLvl;
                    }
                    else
                    {
                        temp += wLvl * 2;
                    }
                }
                if (EquippedWeapon.tertiaryWeaponType != Define.WeaponType.none)
                {
                    wLvl = SelectWeaponLevelType(EquippedWeapon.tertiaryWeaponType).Level;
                    wLvl -= EquippedWeapon.tertiaryWeaponRank;

                    if (wLvl >= 0)
                    {
                        temp += wLvl;
                    }
                    else
                    {
                        temp += wLvl * 2;
                    }
                }
            }
            if(EquippedArmour != null)
            {
                armourTemp -= EquippedArmour.armourComplexity;
                wLvl = SelectWeaponLevelType(EquippedArmour.weaponType).Level;
                wLvl -= EquippedArmour.weaponRank;

                if (wLvl >= 0)
                {
                    armourTemp += wLvl;
                }
                else
                {
                    armourTemp += wLvl * 2;
                }

                if (EquippedArmour.secondaryWeaponType != Define.WeaponType.none)
                {
                    wLvl = SelectWeaponLevelType(EquippedArmour.secondaryWeaponType).Level;
                    wLvl -= EquippedArmour.secondaryWeaponRank;

                    if (wLvl >= 0)
                    {
                        armourTemp += wLvl;
                    }
                    else
                    {
                        armourTemp += wLvl * 2;
                    }
                }
                if (EquippedArmour.tertiaryWeaponType != Define.WeaponType.none)
                {
                    wLvl = SelectWeaponLevelType(EquippedArmour.tertiaryWeaponType).Level;
                    wLvl -= EquippedArmour.secondaryWeaponRank;

                    if (wLvl >= 0)
                    {
                        armourTemp += wLvl;
                    }
                    else
                    {
                        armourTemp += wLvl * 2;
                    }
                }

                if(armourTemp < 0)
                {
                    temp += armourTemp;
                }
            }
            if(temp > 5) // TODO - add skill influence here
            {
                temp = 5;
            }

            return temp;
        }
    }

    public int Rending
    {
        get
        {
            int temp = 0;
            if(EquippedWeapon != null)
            {
                temp += EquippedWeapon.rending;
            }
            return temp;
        }
    }

    public int Range
    {
        get
        {
            int temp = 0;
            if (EquippedWeapon != null)
            {
                temp += EquippedWeapon.range;
            }
            return temp;
        }
    }

    // maximum stats

    // growth rates

    // equipment
    public Weapon EquippedWeapon { get; set; }
    public Armour EquippedArmour { get; set; }
    public HealingMagic EquippedHeal { get; set; }
    public List<Item> CharacterInventory = new List<Item>();

    // weapon ranks
    public List<LevelCounter> weaponRanks;
    public LevelCounter swordRank { get; set; }
    public LevelCounter spearRank { get; set; }
    public LevelCounter AxeRank { get; set; }
    public LevelCounter BowRank { get; set; }
    public LevelCounter ElementalRank { get; set; }
    public LevelCounter DecayRank { get; set; }
    public LevelCounter HealRank { get; set; }
    public LevelCounter armourWeaponRank { get; set; }
    public LevelCounter CreationRank { get; set; }

    // TODO Add current skills list for character

    public List<int> skillIDList { get; set; }
    private List<SkillSO> skillList; 

    public CharacterSO GetCharacterSO()
    {
        if (characterID != -1)
        {
            return Database.Instance.CharacterDictionary[characterID];
        }
        return null;
    }

    public ClassSO GetClassSO()
    {
        return Database.Instance.classDictionary[classID];
    }

    public List<SkillSO> GetSkillList()     // TODO find a solution for not having to make this everytime ( problem is svae/load serialisation)
    {
        if (skillList == null)
        {
            skillList = new List<SkillSO>();
        }
        if(skillIDList == null)
        {
            skillIDList = new List<int>();
        }
        
        if (skillList.Count == 0)
        { 
            foreach (int skillID in skillIDList)
            {
                skillList.Add(Database.Instance.skillDictionary[skillID]);
            }
        }
        

        return skillList;
    }

    public List<SkillSO> GetAllskills()
    {
        List<SkillSO> temp = new List<SkillSO>();

        temp.AddRange(GetSkillList());
        temp.AddRange(GetClassSO().classSkillList);
        if(EquippedWeapon != null)
        {
            temp.AddRange(EquippedWeapon.skillList);
        }
        if (EquippedArmour != null)
        {
            temp.AddRange(EquippedArmour.skillList);
        }

        return GetSkillList(); // TODO return ALL skills from this, from class, weapon, etc.
    }

    public void AddSkill(SkillSO skill)
    {
        GetSkillList().Add(skill);
        skillIDList.Add(skill.skillID);
    }

    public void RemoveSkill(SkillSO skill)
    {
        if (GetSkillList() == null)
        {
            skillList = new List<SkillSO>();
        }
        if (GetSkillList().Contains(skill))
        {
            GetSkillList().Remove(skill);
        }
        if (skillIDList.Contains(skill.skillID))
        {
            skillIDList.Remove(skill.skillID);
        }
    }

    public Sprite GetCharacterSprite()
    {
        if(GetCharacterSO() == null)
        {
            return GetClassSO().genericClassSprite;
        }
        else
        {
            return GetCharacterSO().characterSprite;
        }
    }

    public Character()  // only used for loading perposes - do not use
    {

    }
    public Character(CharacterSO character)
    {
        ClassSO startingClass = character.baseClass;
        classID = startingClass.classID;
        // TODO - review character constructor
        characterID = character.characterID;
        characterName = character.characterName;
        unitAllignment = character.unitAllignment;
        unitType = startingClass.unitType;
        AIType = character.AIType;
        secondaryAIType = character.SecondaryAIType;
        battalionNumber = character.battalionNumber;

        // starting stats

        int hp = character.baseHP + startingClass.baseHP;
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
        HP = new BaseStat(hp, hpGrowth, MaximumHP);
        UnmodifiedStrength = new BaseStat(str, StrengthGrowth, MaxStrength);
        UnmodifiedMagic = new BaseStat(mag, MagicGrowth, MaxMagic);
        UnmodifiedOffence = new BaseStat(off, OffenceGrowth, MaxOffence);
        UnmodifiedDefence = new BaseStat(def, DefenceGrowth, MaxDefence);
        UnmodifiedResistance = new BaseStat(res, ResistanceGrowth, MaxResistance);
        UnmodifiedSpeed = new BaseStat(spd, SpeedGrowth, MaxSpeed);
        Move = new BaseStat(mov, MoveGrowth, MaxMove);

        Level = new LevelCounter(character.level, 1, Define.WeaponType.none);

        // assign weapon ranks
        swordRank = new LevelCounter(character.swordRank, character.swordMastery, Define.WeaponType.Sword);
        spearRank = new LevelCounter(character.spearRank,character.spearMastery, Define.WeaponType.Polearm);
        AxeRank = new LevelCounter(character.axeRank,character.axeMastery, Define.WeaponType.Axe);
        BowRank = new LevelCounter(character.bowRank,character.bowMastery, Define.WeaponType.Bow);
        ElementalRank = new LevelCounter(character.elementalRank,character.elementalMastery, Define.WeaponType.Elemental);
        DecayRank = new LevelCounter(character.decayRank,character.bowMastery, Define.WeaponType.Decay);
        HealRank = new LevelCounter(character.healRank,character.healMastery, Define.WeaponType.Healing);
        armourWeaponRank = new LevelCounter(character.armourWeaponRank,character.armourWeaponMastery, Define.WeaponType.Armour);

        // add to weaponranks list
        weaponRanks = new List<LevelCounter>();
        weaponRanks.Add(swordRank);
        weaponRanks.Add(spearRank);
        weaponRanks.Add(AxeRank);
        weaponRanks.Add(BowRank);
        weaponRanks.Add(ElementalRank);
        weaponRanks.Add(DecayRank);
        weaponRanks.Add(HealRank);
        weaponRanks.Add(armourWeaponRank);

        CreationRank = new LevelCounter(character.creationRank,character.creationMastery, Define.WeaponType.Creation); // TODO - implement creation rank somewhere? 
        //weaponRanks.Add(swordRank);

        // equip weapon, armour and set inventory
        if (character.equippedStartingWeapon != null)
        {
            Weapon tempWeapon = new Weapon(character.equippedStartingWeapon);

            if (CanEquipWeapon(tempWeapon))
            {
                EquipWeapon(tempWeapon);

            }
            CharacterInventory.Add(tempWeapon);
            
        }

        if(character.equippedStartingArmour != null)
        {
            // TODO - implement canEquipArmour function and implent here, as above
            Armour tempArmour = new Armour(character.equippedStartingArmour);

            if (CanEquipArmour(tempArmour))
            {
                EquipArmour(tempArmour);
            }
            CharacterInventory.Add(tempArmour);

        }
        
        foreach (ItemSO itemSO in character.startingInventory)
        {
            CharacterInventory.Add(itemSO.CreateItem());
        }

        // add skills
        //TODO - double check assigning skills
        foreach(SkillSO skill in startingClass.classSkillList)
        {
            AddSkill(skill);
        }
        foreach(SkillSO skill in character.uniqueSkillList)
        {
            AddSkill(skill);
        }

    }

    public Character(Define.GenericEnemyData unitData) // constructor for genric units including difficulty options to influence stats
    {
        Level = new LevelCounter(unitData.level, 1, Define.WeaponType.none);
        characterID = -1;
        ClassSO classSO = unitData.unitClass;
        classID = classSO.classID;
        characterName = classSO.className;
        unitAllignment = unitData.allignment;
        unitType = classSO.unitType;
        AIType = unitData.AIType;
        secondaryAIType = unitData.SecondaryAIType; // TODO sort out the relationship between data stored in unitcontroller and char = unitcontroller should contain map specific information
        battalionNumber = unitData.battalionNumber;

        // assign weapon ranks
        // 

        int weaponScaling = Mathf.FloorToInt(Level.Level / 2);

        swordRank = new LevelCounter(classSO.swordRank + weaponScaling, classSO.swordMastery, Define.WeaponType.Sword); // CONSIDER - add a dificulty option for enemy weapon ranks and implement here
        spearRank = new LevelCounter(classSO.spearRank + weaponScaling, classSO.spearMastery, Define.WeaponType.Polearm);
        AxeRank = new LevelCounter(classSO.axeRank + weaponScaling, classSO.axeMastery, Define.WeaponType.Axe);
        BowRank = new LevelCounter(classSO.bowRank + weaponScaling, classSO.bowMastery, Define.WeaponType.Bow);
        ElementalRank = new LevelCounter(classSO.elementalRank + weaponScaling, classSO.elementalMastery, Define.WeaponType.Elemental);
        DecayRank = new LevelCounter(classSO.decayRank + weaponScaling, classSO.decayMastery, Define.WeaponType.Decay);
        HealRank = new LevelCounter(classSO.healRank + weaponScaling, classSO.healMastery, Define.WeaponType.Healing);
        armourWeaponRank = new LevelCounter(classSO.armourWeaponRank + weaponScaling, classSO.armourWeaponMastery, Define.WeaponType.Armour);
        CreationRank = new LevelCounter(classSO.creationRank + weaponScaling, classSO.creationMastery, Define.WeaponType.Creation);

        // add to weaponranks list 
        weaponRanks = new List<LevelCounter>();
        weaponRanks.Add(swordRank);
        weaponRanks.Add(spearRank);
        weaponRanks.Add(AxeRank);
        weaponRanks.Add(BowRank);
        weaponRanks.Add(ElementalRank);
        weaponRanks.Add(DecayRank);
        weaponRanks.Add(HealRank);
        weaponRanks.Add(armourWeaponRank);
        // TODO - add creation?


        CharacterInventory = new List<Item>(); // is this neccessary?
        EquippedWeapon = new Weapon(unitData.equippedWeapon);
        CharacterInventory.Add(EquippedWeapon);
        EquippedArmour = new Armour(unitData.equippedArmour);
        CharacterInventory.Add(EquippedArmour);
        //CharacterInventory = unitData.startingInventory;
        foreach (ItemSO itemSO in unitData.startingInventory)
        {
            CharacterInventory.Add(itemSO.CreateItem());
        }

        // add skills
        //TODO - double check assigning skills
        foreach (SkillSO skill in classSO.classSkillList)
        {
            AddSkill(skill);
        }


        // TODO add difficulty options and static options class, bool IsTough, bool IsSuperTough, bool IsStrong, bool IsSuperStrong, modifying the below ints
        int hPDifficultyModifier = 0;
        int strengthDifficultyModifier =0;
        int magicDifficultyModifier =0;
        int offenceDifficultyModifier =0 ;
        int defenceDifficultyModifier =0;
        int resistanceDifficultyModifier =0;
        int speedDifficultyModifier =0;

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
        int hP = Mathf.Min(classSO.maxHP, Mathf.RoundToInt(classSO.baseHP + (Level.Level-1) * hPGrowth / 100f));
        int str = Mathf.Min(classSO.maxStrength, Mathf.RoundToInt(classSO.baseStrength + (Level.Level - 1) * StrengthGrowth / 100f));
        int mag = Mathf.Min(classSO.maxMagic, Mathf.RoundToInt(classSO.baseMagic + (Level.Level - 1) * MagicGrowth / 100f));
        int off = Mathf.Min(classSO.maxOffence, Mathf.RoundToInt(classSO.baseOffence + (Level.Level - 1) * OffenceGrowth / 100f));
        int def = Mathf.Min(classSO.maxDefence, Mathf.RoundToInt(classSO.baseDefence + (Level.Level - 1) * DefenceGrowth / 100f));
        int res = Mathf.Min(classSO.maxResistance, Mathf.RoundToInt(classSO.baseResistance + (Level.Level - 1) * ResistanceGrowth / 100f));
        int spd = Mathf.Min(classSO.maxSpeed, Mathf.RoundToInt(classSO.baseSpeed + (Level.Level - 1) * SpeedGrowth / 100f));
        int mov = classSO.baseMove;

        // assign unmodified stats

        HP = new BaseStat(hP, hPGrowth, MaximumHP);
        UnmodifiedStrength = new BaseStat(str, StrengthGrowth, MaxStrength);
        UnmodifiedMagic = new BaseStat(mag, MagicGrowth, MaxMagic);
        UnmodifiedOffence = new BaseStat(off, OffenceGrowth, MaxOffence);
        UnmodifiedDefence = new BaseStat(def, DefenceGrowth, MaxDefence);
        UnmodifiedResistance = new BaseStat(res, ResistanceGrowth, MaxResistance);
        UnmodifiedSpeed = new BaseStat(spd, SpeedGrowth, MaxSpeed);
        Move = new BaseStat(mov, MoveGrowth, MaxMove);


    }

    private IEnumerator LevelUp()
    {
        // CONSIDER alternate lower-RNG level up method option
        LevelUpScreenManager levelUpScreen = GameManager.Instance.LevelUpScreenManager;

        int hpIncrease =0, strIncrease = 0, magIncrease = 0, offIncrease = 0, defIncrease = 0, resIncrease = 0, spdIncrease = 0;

        // increase stats

        hpIncrease = HP.LevelUp();
        strIncrease = UnmodifiedStrength.LevelUp();
        magIncrease = UnmodifiedMagic.LevelUp();
        offIncrease = UnmodifiedOffence.LevelUp();
        defIncrease = UnmodifiedDefence.LevelUp();
        resIncrease = UnmodifiedResistance.LevelUp();
        spdIncrease = UnmodifiedSpeed.LevelUp();

        // show level- up screen
        yield return GameManager.Instance.StartCoroutine(levelUpScreen.OpenMenu(this, hpIncrease, strIncrease, magIncrease, offIncrease, defIncrease, resIncrease, spdIncrease));

        yield return new WaitForSeconds(0.5f);

        yield return GameManager.Instance.StartCoroutine(Utils.WaitForMouseClick());

        levelUpScreen.CloseMenu();

        yield break;
    }

    public void ClassChange()
    {
        // TODO implement Character Class change method
    }

    // also include method for calculating the amount of experience to be gained

    private IEnumerator GainExperience(int amount)
    {
        // TODO implement max level and possible class change at certain levels+
        if (Level.GainExperience(amount))
        {
            yield return GameManager.Instance.StartCoroutine(LevelUp());
        }

        yield return GameManager.Instance.StartCoroutine(testMethod());

        yield break;
    }

    public IEnumerator TriggerExperienceGain(UnitController unit)
    {
        if(unitAllignment != Define.UnitAllignment.Player)
        {
            yield break;
        }

        int expGainAmount = CalcultateExpFromUnit(unit);
        int wexpGainAmount = 0;
        LevelCounter pWeaponRank = null;
        LevelCounter sWeaponRank = null;
        LevelCounter tWeaponRank = null;

        if (EquippedWeapon != null)
        {
            wexpGainAmount = CalcultateWExpFromUnit(unit);

            pWeaponRank = SelectWeaponLevelType(EquippedWeapon.weaponType);

            if (pWeaponRank.GainExperience(wexpGainAmount))
            {
                GameManager.Instance.eventMessage.OpenMenu("Weapon level increased");
            }

            if (EquippedWeapon.secondaryWeaponType != Define.WeaponType.none)
            {
                sWeaponRank = SelectWeaponLevelType(EquippedWeapon.secondaryWeaponType);
                if (sWeaponRank.GainExperience(wexpGainAmount))
                {
                    GameManager.Instance.eventMessage.OpenMenu("Weapon level increased");
                }
            }
            if (EquippedWeapon.tertiaryWeaponType != Define.WeaponType.none)
            {
                tWeaponRank = SelectWeaponLevelType(EquippedWeapon.tertiaryWeaponType);
                if (tWeaponRank.GainExperience(wexpGainAmount))
                {
                    GameManager.Instance.eventMessage.OpenMenu("Weapon level increased");
                }
            }

            // OpenMenu()
        }

        yield return GameManager.Instance.StartCoroutine(GameManager.Instance.expDisplay.OpenMenu(Level, expGainAmount, pWeaponRank, wexpGainAmount, sWeaponRank, wexpGainAmount, tWeaponRank, wexpGainAmount));

        
        yield return GameManager.Instance.StartCoroutine(GainExperience(expGainAmount));
    }

    private int CalcultateWExpFromUnit(UnitController unit)
    {
        int wexpAmount = unit.Character.CalculateWeaponProficiency() - CalculateWeaponProficiency() +1;

        int weaponTypesNumber = 1;

        if(EquippedWeapon.secondaryWeaponType != Define.WeaponType.none)
        {
            weaponTypesNumber++;
        }
        if (EquippedWeapon.tertiaryWeaponType != Define.WeaponType.none)
        {
            weaponTypesNumber++;
        }

        if (unit.IsDestroyed)
        {
            wexpAmount += wexpAmount;
        }

        wexpAmount = Mathf.CeilToInt((float)wexpAmount / weaponTypesNumber);
        wexpAmount *= Define.WEXPMULTPLIER;

        // CONSDIER option to turn off weapon exp from not destroyed units

        return wexpAmount;
    }

    public int CalculateWeaponProficiency()
    {
        int weaponTypesNumber = 1;
        if(EquippedWeapon == null)
        {
            return 0;
        }

        int weaponexp = SelectWeaponLevelType(EquippedWeapon.weaponType).Level;

        if(EquippedWeapon.secondaryWeaponType != Define.WeaponType.none)
        {
            weaponexp += SelectWeaponLevelType(EquippedWeapon.secondaryWeaponType).Level;
            weaponTypesNumber++;
        }
        if (EquippedWeapon.tertiaryWeaponType != Define.WeaponType.none)
        {
            weaponexp += SelectWeaponLevelType(EquippedWeapon.tertiaryWeaponType).Level;
            weaponTypesNumber++;
        }

        return Mathf.FloorToInt((float)weaponexp / weaponTypesNumber);
    }

    private int CalcultateExpFromUnit(UnitController unit)
    {
        if (unit.IsDestroyed)
        {
            return (Level.Level - unit.Character.Level.Level + 5) * Define.DESTROYEDENEMYEXPMULTPLIER;
        }
        else
        {
            return Level.Level - unit.Character.Level.Level + 5;
        }
    }

    public IEnumerator TriggerExperienceGainLevelOnly(int amount)
    {
        yield return GameManager.Instance.StartCoroutine(GainExperience(amount));
    }

    public bool CanEquipWeapon(Weapon weapon)
    {
        bool canEquip = false;

        if (weapon.weaponType == Define.WeaponType.none)
        {
            return canEquip;
        }
        LevelCounter rankToCheck = SelectWeaponLevelType(weapon.weaponType);
        if (rankToCheck.MasteryLevel >= weapon.weaponMasteryLevel)
        {
            canEquip = true;
        }

        if(weapon.secondaryWeaponType == Define.WeaponType.none)
        {
            return canEquip;
        }
        rankToCheck = SelectWeaponLevelType(weapon.secondaryWeaponType);
        if (rankToCheck.MasteryLevel >= weapon.weaponMasteryLevel)
        {
            canEquip = true;
        }

        if (weapon.tertiaryWeaponType == Define.WeaponType.none)
        {
            return canEquip;
        }
        rankToCheck = SelectWeaponLevelType(weapon.tertiaryWeaponType);
        if (rankToCheck.MasteryLevel >= weapon.weaponMasteryLevel)
        {
            canEquip = true;
        }

        return canEquip;
    }

    public void EquipWeapon(Weapon weapon)
    {
        if (CanEquipWeapon(weapon))
        {
            EquippedWeapon = weapon;
        }
        // add equipped weapon to inventory
        // set equipped weapon to equipped weapon
    }

    public void UnEquipWeapon()     // could equip a fists weapon instead?
    {
        EquippedWeapon = null;
    }

    public void EquipArmour(Armour armour)
    {
        if (CanEquipArmour(armour))
        {
            EquippedArmour = armour;
        }
    }

    public void UnEquipArmour()
    {
        EquippedArmour = null;
    }

    public bool CanEquipArmour(Armour armour)
    {
        // CONSIDER - add allowedArmourList variable to character?
        List<Define.ArmourType> allowedList = Database.Instance.classDictionary[classID].allowedArmourTypes;
        if (allowedList.Contains(armour.allowedArmourUser))
        {
            return true;
        }
        return false;
    }

    public void OnItemUse(Item item)
    {
        if (item.ReduceDurability()) // true if item use <1
        {
            if (EquippedWeapon == item)
            {
                UnEquipWeapon();
            }
            CharacterInventory.Remove(item);
        }
    }
    public void EquipHeal(HealingMagic heal)
    {
        EquippedHeal = heal;
        // add equipped weapon to inventory
        // set equipped weapon to equipped weapon
    }

    public void AddItemToInventory(Item item)
    {
        if(CharacterInventory.Count < 8)
        {
            CharacterInventory.Add(item);
        } else
        {
            GameManager.Instance.playerData.AddInventoryItem(item);
        }
    }

    public void RemoveItemFromInventory(Item item)
    {
        if (CharacterInventory.Contains(item))
        {
            CharacterInventory.Remove(item);
            if (EquippedWeapon == item)
            {
                UnEquipWeapon();
            }
            if (EquippedArmour == item)
            {
                UnEquipArmour();
            }
        }
    }

    public void AddItemToInventory(ItemSO itemSO)
    {
        if (CharacterInventory.Count < 8)
        {
            CharacterInventory.Add(itemSO.CreateItem());
        } else
        {
            GameManager.Instance.playerData.AddInventoryItem(itemSO.CreateItem()); // TODO - check if playerdata add inventory item has a limit
        }
    }

    private IEnumerator testMethod()
    {
        //Debug.Log("Test method okay"); // TODO - figure out why this method is neccessary
        yield break;
    }

    private LevelCounter SelectWeaponLevelType(Define.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case Define.WeaponType.Sword:
                return swordRank;
            case Define.WeaponType.Polearm:
                return spearRank;
            case Define.WeaponType.Axe:
                return AxeRank;
            case Define.WeaponType.Bow:
                return BowRank;
            case Define.WeaponType.Elemental:
                return ElementalRank;
            case Define.WeaponType.Decay:
                return DecayRank;
            case Define.WeaponType.Armour:
                return armourWeaponRank;
            case Define.WeaponType.Healing:
                return HealRank;
            case Define.WeaponType.Creation:
                return CreationRank;
            default:
                return null;
        }
    }

    public int DetermineSkillBonusDamage(int damage, int rending, int skillroll, out int newRending)  // maybe add status or change function
    {
        int newDamage = damage;
        newRending = rending;

        foreach(SkillSO skill in GetAllskills())
        {
            newDamage = skill.OffensiveSkillActivationBonus(newDamage, newRending, skillroll, out newRending);
        }

        return newDamage;

    }

}
