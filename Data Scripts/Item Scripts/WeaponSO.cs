using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Weapon")]
public class WeaponSO : ItemSO
{
    public Define.WeaponType weaponType;
    public int weaponRank;
    public int weaponMasteryLevel;
    public Define.WeaponType secondaryWeaponType;
    public int secondaryWeaponRank;
    public int secondaryWeaponMasteryLevel;
    public Define.WeaponType tertiaryWeaponType;
    public int tertiaryWeaponRank;
    public int tertiaryWeaponMasteryLevel;

    public bool IsMagical; // does the weapon deal magic damage? (attacking RES not AMR)
    public int power;
    //public int hitrate;
    public int rending;
    public int criticalRate;
    public int offence;
    public int defence;
    public int weight;
    
    public int range;
    // bonus stats
    public int bonusStrength;
    public int bonusMagic;
    public int bonusSpeed;
    public int bonusArmour;
    public int bonusResistance;

    public AudioClip onAttackAudio;

    public override Item CreateItem()
    {
        return new Weapon(this);
    }

    public override void OnUse(UnitController unit)
    {
        throw new System.NotImplementedException();
    }
}
