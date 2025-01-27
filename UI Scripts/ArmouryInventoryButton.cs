using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ArmouryInventoryButton : MonoBehaviour, IPointerEnterHandler
{
    public PCArmouryManager armouryManager;
    public Image itemIcon;
    public TextMeshProUGUI itemName, itemDurability, itemCost, itemQuantity;
    public ShopItem shopItem;
    public Item item;
    public Image backgroundImage;


    public void DisplayItem(ShopItem shopItem)
    {
        ItemSO itemSO = shopItem.GetItem();

        itemIcon.sprite = itemSO.itemIcon;
        itemName.text = itemSO.itemName;
        if (itemSO.IsUnbreakable)
        {
            itemDurability.text = "--/--";
        }
        else
        {
            itemDurability.text = itemSO.itemMaxDurability + "/" + itemSO.itemMaxDurability; // CONSIDER using stringbuilder here or having seperate text?
        }
        
        itemCost.text = shopItem.cost.ToString(); 
        itemQuantity.text = shopItem.quantity.ToString();
        this.shopItem = shopItem;
        item = null;

        if (itemSO is WeaponSO)
        {
            backgroundImage.sprite = Database.Instance.weaponButtonBackground;
        }
        else if (itemSO is ArmourSO)
        {
            backgroundImage.sprite = Database.Instance.armourButtonBackground;
        }
        else
        {
            backgroundImage.sprite = Database.Instance.otherItemButtonBackground;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        armouryManager.OnButtonHover(this);
    }

    public Item GenerateItem()
    {
        if(item == null)
        {
            item = shopItem.GetItem().CreateItem();
        }

        return item;
    }
}
