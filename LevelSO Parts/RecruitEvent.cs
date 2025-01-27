using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Assets/Map Events/RecruitEvent")]
public class RecruitEvent : MapEventSO
{
    public CharacterSO characterSO;
    public override IEnumerator TriggerEvent(UnitController unit, Vector3Int location)
    {
         GameManager gameManager = GameManager.Instance;
         Character newUnit = new Character(characterSO);
         Vector3Int spawnLocation = gameManager.FindNearestEmptyTile(gameManager.levelMapManager.GetValue(unit.Location), newUnit.unitType).MapLocation;
         
         gameManager.playerData.AddPlayerCharacter(newUnit);
         gameManager.AddCharacterUnit(newUnit, spawnLocation);

        yield return gameManager.StartCoroutine(gameManager.eventMessage.OpenMenu(characterSO.characterName + " recruited!"));
        ReplaceAestheticTile(location);
        yield break;

    }
}
