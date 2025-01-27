using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscItemSO : ItemSO
{
    public override Item CreateItem()
    {
        return new MiscItem(this);
    }

    public override void OnUse(UnitController unit)
    {
        
    }
}
