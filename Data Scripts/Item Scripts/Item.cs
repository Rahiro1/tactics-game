using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item 
{
    public int itemID { get; protected set; }
    public string ItemName { get; protected set; }
    public string ItemDescription { get; protected set; }
    public int ItemBaseCost { get; protected set; }
    public int ItemCurrentDurability { get; protected set; }
    public int ItemMaxDurability { get; protected set; }
    public bool IsUseable { get; protected set; }
    public bool IsUnbreakable { get; protected set; }

    public List<int> skillIDList { get; protected set; }
    public List<SkillSO> skillList { get; protected set; }

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

    [JsonConstructor]
    public Item(int itemID, string itemName, string itemDescription, int itemBaseCost, int itemCurrentDurability, int itemMaxDurability, bool isUseable, bool isUnbreakable, List<int> skillIDList, List<SkillSO> skillList)
    {
        this.itemID = itemID;
        ItemName = itemName;
        ItemDescription = itemDescription;
        ItemBaseCost = itemBaseCost;
        ItemCurrentDurability = itemCurrentDurability;
        ItemMaxDurability = itemMaxDurability;
        IsUseable = isUseable;
        IsUnbreakable = isUnbreakable;
        this.skillIDList = skillIDList;
        this.skillList = skillList;
    }
}
