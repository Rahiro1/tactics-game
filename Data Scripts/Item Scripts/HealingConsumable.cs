using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingConsumable : Item
{
    public int healAmount;
    public int healArmourAmount;
    public HealingConsumable(HealingConsumableSO template) : base(template)
    {
        healAmount = template.healAmount;
        healArmourAmount = template.healArmourAmount;
    }

    public HealingConsumable() : base()
    {

    }
}
