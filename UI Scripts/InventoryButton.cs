using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryButton : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI itemName, itemDurability;
    public Item item;
    public Image backgroundImage;
    public GameObject equippedIcon;


    public void DisplayItem(Item item, bool isEquipped)
    {
        itemIcon.sprite = item.GetItemSO().itemIcon;
        itemName.text = item.ItemName;
        if (item.IsUnbreakable)
        {
            itemDurability.text = "--/--";
        }
        else
        {
            itemDurability.text = item.ItemCurrentDurability + "/" + item.ItemMaxDurability; // CONSIDER using stringbuilder here or having seperate text?
        }
        if(backgroundImage != null)
        {
            if (item is Weapon)
            {
                backgroundImage.sprite = Database.Instance.weaponButtonBackground;
            }
            else if (item is Armour)
            {
                backgroundImage.sprite = Database.Instance.armourButtonBackground;
            }
            else
            {
                backgroundImage.sprite = Database.Instance.otherItemButtonBackground;
            }
        }
        


        
        this.item = item;
        if(equippedIcon == null)
        {
            return;
        }
        if (isEquipped)
        {
            equippedIcon.SetActive(true);
        }
        else
        {
            equippedIcon.SetActive(false);
        }
    }
}
