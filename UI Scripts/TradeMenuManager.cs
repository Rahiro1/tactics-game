using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeMenuManager : MonoBehaviour
{
    public PCInventoryMenuManager sourceInventory, targetInventory;
    private Character source, target;


    public void OpenMenu(UnitController sourceUnit, UnitController targetUnit)
    {
        source = sourceUnit.Character;
        target = targetUnit.Character;
        ReloadInventories();
        gameObject.transform.position = sourceUnit.transform.position + GameManager.Instance.tileMapGrid.cellSize.y * Vector3.right;
        gameObject.SetActive(true);

    }

    public void ReloadInventories()
    {
        sourceInventory.LoadCharacterInventory(source);
        targetInventory.LoadCharacterInventory(target);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void SourceInventroryButtonClicked(InventoryButton button)
    {
        if(target.CharacterInventory.Count < 8)
        {
            source.RemoveItemFromInventory(button.item);
            target.AddItemToInventory(button.item);
            ReloadInventories();
        }
    }

    public void TargetInventroryButtonClicked(InventoryButton button)
    {
        if(source.CharacterInventory.Count < 8)
        {
            target.RemoveItemFromInventory(button.item);
            source.AddItemToInventory(button.item);
            ReloadInventories();
        }
    }

}
