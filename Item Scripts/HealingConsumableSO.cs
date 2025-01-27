using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Items/Other/Consumable/Healing")]
public class HealingConsumableSO : ItemSO
{
    public int healAmount;
    public int healArmourAmount;

    public override Item CreateItem()
    {
        return new HealingConsumable(this);
    }

    public override void OnUse(UnitController unit)
    {
        unit.RecieveHeal(healAmount, healArmourAmount);
    }
}
