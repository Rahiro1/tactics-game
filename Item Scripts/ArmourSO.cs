using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Assets/Items/Armour")]
public class ArmourSO : ItemSO
{ 
    public Define.ArmourType allowedArmourUser;
    public int armourValue;
    public int armourOffence;
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

    public override Item CreateItem()
    {
        return new Armour(this);
    }

    public override void OnUse(UnitController unit)
    {
        throw new System.NotImplementedException();
    }
}
