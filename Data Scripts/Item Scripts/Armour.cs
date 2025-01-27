using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armour : Item
{
    public Define.ArmourType allowedArmourUser;

    public int armourValue;
    public int armourOffence;   // consider making these bonus off/def
    public int armourDefence;
    public int armourComplexity;

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

    public Armour(ArmourSO template) : base(template)
    {
        allowedArmourUser = template.allowedArmourUser;
        armourValue = template.armourValue;
        armourOffence = template.armourOffence;
        armourDefence = template.armourDefence;
        armourComplexity = template.armourComplexity;

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

    public Armour() : base()
    {

    }

}
