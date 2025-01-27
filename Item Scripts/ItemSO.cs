using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemIcon;
    public int itemBaseCost;
    public int itemCurrentDurability;
    public int itemMaxDurability;
    public bool IsUseable;
    public bool IsUnbreakable;

    public List<SkillSO> skillList;

    public abstract void OnUse(UnitController unit);

    public abstract Item CreateItem();
}
