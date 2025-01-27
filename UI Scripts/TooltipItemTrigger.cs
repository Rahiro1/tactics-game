using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipItemTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryButton inventoryButton;
    public ArmouryInventoryButton armouryInventoryButton;
    public ShopInventoryButton shopInventoryButton;
    public Vector3 offset;
    public TooltipWeapon weaponTooltip;
    public TooltipArmour armourTooltip;
    public TooltipBasic otherItemTooltip;
    public float tooltipDelay = 0.5f;
    private WaitForSeconds waitCache;
    private IEnumerator coroutine;

    void Start()
    {
        waitCache = new WaitForSeconds(tooltipDelay);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        coroutine = TooltipWait();
        StartCoroutine(coroutine);
    }

    public IEnumerator TooltipWait()
    {
        yield return waitCache;


        Vector3 position;
        Item item;
        if (inventoryButton != null)
        {
            item = inventoryButton.item;
            position = inventoryButton.transform.position + offset;
        } else if(armouryInventoryButton != null)
        {
            item = armouryInventoryButton.GenerateItem();
            position = armouryInventoryButton.transform.position + offset;
        } else
        {
            item = shopInventoryButton.GenerateItem();
            position = shopInventoryButton.transform.position + offset;
        }
        

        if (item is Weapon weapon)
        {
            weaponTooltip.ShowTooltip(weapon, position);
        } else if (item is Armour armour)
        {
            armourTooltip.ShowTooltip(armour, position);
        } else if(item != null)
        {
            otherItemTooltip.ShowTooltip(item.ItemName, item.ItemDescription, position);
        }

        
        yield break;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDisable();
    }

    private void OnDisable()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        weaponTooltip.HideTooltip();
        armourTooltip.HideTooltip();
        otherItemTooltip.HideTooltip();
    }
}