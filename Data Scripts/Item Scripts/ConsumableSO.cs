using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableSO : ItemSO
{
    public override Item CreateItem()
    {
        return new Consumable(this);
    }

    // TODO consumableSO
    public override void OnUse(UnitController unit)
    {
        throw new System.NotImplementedException();
    }
}
