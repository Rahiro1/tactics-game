using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem
{
    public int itemID;
    public int quantity;
    public int cost;
    public int timeCost;

    public ShopItem(Define.ShopData shopStruct)
    {
        itemID = shopStruct.item.itemID;
        quantity = shopStruct.quantity;
        cost = shopStruct.cost;
        timeCost = shopStruct.timeCost;
    }

    public ShopItem()
    {

    }

    public ItemSO GetItem()
    {
        return Database.Instance.itemDictionary[itemID];
    }
}
