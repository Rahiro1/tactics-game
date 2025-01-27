using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/HealingMagic")]
public class HealingMagicSO : ItemSO
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

    public override Item CreateItem()
    {
        return new HealingMagic(this);
    }

    public override void OnUse(UnitController unit)
    {
        throw new System.NotImplementedException();
    }
}
