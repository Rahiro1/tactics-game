using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Assets/Map Events/Money")]
public class MapEventMoney : MapEventSO
{
    public int goldToGain;
    public string message;

    public override IEnumerator TriggerEvent(UnitController unit, Vector3Int location)
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.playerData.AdjustGold(goldToGain);
        yield return gameManager.StartCoroutine(gameManager.eventMessage.OpenMenu(message));
        ReplaceAestheticTile(location);
        yield break;
    }
}
