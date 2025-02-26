using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Assets/Map Events/ItemEvent")]
public class GetItemEventSO : MapEventSO
{
    public ItemSO itemToGet;
    public string message;
    public override IEnumerator TriggerEvent(UnitController unit, Vector3Int location)
    {
        GameManager gameManager = GameManager.Instance;
        unit.Character.AddItemToInventory(itemToGet);
        yield return gameManager.StartCoroutine(gameManager.eventMessage.OpenMenu(message));
        ReplaceAestheticTile(location);
    }
}
