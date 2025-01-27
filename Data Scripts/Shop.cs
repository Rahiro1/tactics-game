using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public int level;
    public List<ShopItem> shopItems;


    public Shop(ShopSO shopSO)
    {
        shopItems = new List<ShopItem>();

        foreach(Define.ShopData shopData in shopSO.shop)
        {
            shopItems.Add(new ShopItem(shopData));
        }
    }

    public Shop()
    {

    }
}
