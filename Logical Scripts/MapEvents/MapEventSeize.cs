using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Map Events/Seize")]
public class MapEventSeize : MapEventSO
{
    // only the unit with the correct ID can trigger the event
    public int iDToActivate;
    public string message = "Area seized";
    public override IEnumerator TriggerEvent(UnitController unit, Vector3Int location)
    {
        GameManager gameManager = GameManager.Instance;
        if (unit.Character.characterID == iDToActivate)
        {
            yield return gameManager.StartCoroutine(gameManager.eventMessage.OpenMenu(message));
            gameManager.SetState(new VictoryState(gameManager));
            yield break;
        }
    }
}
