using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingMagic : Item
{
    public int power;
    public int armourHealing;
    public int weight;
    public int weaponRank;
    public int range;
    public int maximumDurability;

    public Define.WeaponType weaponType;
    public Define.WeaponType secondaryWeaponType;
    public Define.WeaponType tertiaryWeaopnType;
    public HealingMagic(HealingMagicSO template) : base(template)
    {
        power = template.power;
        armourHealing = template.armourHealing;
        weight = template.weight;
        weaponRank = template.weaponRank;
        range = template.range;
        maximumDurability = template.maximumDurability;

        weaponType = template.weaponType;
        secondaryWeaponType = template.secondaryWeaponType;
        tertiaryWeaopnType = template.tertiaryWeaopnType;
    }

    public virtual int GetModifiedPower(Character character)
    {
        return power + Mathf.FloorToInt(character.Magic.GetModifiedValue()/2f);
    }

    public virtual int GetModifiedRange(Character character)
    {
        return range;
    }


}
