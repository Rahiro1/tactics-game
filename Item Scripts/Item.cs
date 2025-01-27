using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
    public int itemID { get; set; }
    public string ItemName { get; set; }
    public string ItemDescription { get; set; }
    public int ItemBaseCost { get; set; }
    public int ItemCurrentDurability { get; set; }
    public int ItemMaxDurability { get; set; }
    public bool IsUseable { get; set; }
    public bool IsUnbreakable { get; set; }

    public List<int> skillIDList;
    public List<SkillSO> skillList;

    public void OnUse(UnitController unit)
    {
        GetItemSO().OnUse(unit);
        unit.Character.OnItemUse(this);
    }

    public ItemSO GetItemSO()
    {
        return Database.Instance.itemDictionary[itemID];
    }

    public bool ReduceDurability()
    {
        if (IsUnbreakable)
        {
            return false;
        }

        ItemCurrentDurability -= 1;
        if(ItemCurrentDurability < 1)
        {
            return true;
        }

        return false;
    }

    public List<SkillSO> GetSkillList()     // TODO find a solution for not having to make this everytime ( problem is svae/load serialisation)
    {
        if (skillList == null)
        {
            skillList = new List<SkillSO>();
        }
        if (skillIDList == null)
        {
            skillIDList = new List<int>();
        }

        if (skillList.Count == 0)
        {
            foreach (int skillID in skillIDList)
            {
                skillList.Add(Database.Instance.skillDictionary[skillID]);
            }
        }


        return skillList;
    }

    public Item(ItemSO template)
    {
        itemID = template.itemID;
        ItemName = template.itemName;
        ItemBaseCost = template.itemBaseCost;       // consider if this is needed
        ItemDescription = template.itemDescription;
        ItemCurrentDurability = template.itemMaxDurability;
        ItemMaxDurability = template.itemMaxDurability;
        IsUseable = template.IsUseable;
        IsUnbreakable = template.IsUnbreakable;
        skillList = template.skillList;
        skillIDList = new List<int>();
        foreach(SkillSO skill in skillList)
        {
            skillIDList.Add(skill.skillID);
        }

    }

    public Item() : base()
    {

    }

}
