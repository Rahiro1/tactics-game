using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    //public WeaponSO weaponSO;
    public bool IsMagical;
    public int power;
    //public int hitRate; hitrate neccesity under review
    public int rending;
    public int criticalRate;
    public int offence;
    public int defence;
    public int complexity;
    public int range;

    public int bonusStrength;
    public int bonusMagic;
    public int bonusSpeed;
    public int bonusArmour;
    public int bonusResistance;

    public Define.WeaponType weaponType;
    public int weaponRank;
    public int weaponMasteryLevel;
    public Define.WeaponType secondaryWeaponType;
    public int secondaryWeaponRank;
    public int secondaryWeaponMasteryLevel;
    public Define.WeaponType tertiaryWeaponType;
    public int tertiaryWeaponRank;
    public int tertiaryWeaponMasteryLevel;
    

    public Weapon(WeaponSO template) : base(template)
    {
        IsMagical = template.IsMagical;
        power = template.power;
        //hitRate = template.hitRate;
        rending = template.rending;
        criticalRate = template.criticalRate;
        offence = template.offence;
        defence = template.defence;
        complexity = template.weight;
        range = template.range;

        bonusStrength = template.bonusStrength;
        bonusMagic = template.bonusMagic;
        bonusSpeed = template.bonusSpeed;
        bonusArmour = template.bonusArmour;
        bonusResistance = template.bonusResistance;

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

    public Weapon() : base()
    {

    }

    // TODO consider forging system
}
