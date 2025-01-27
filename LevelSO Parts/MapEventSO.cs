using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class MapEventSO : ScriptableObject
{
    public bool canLoot = false;
    public Sprite alternateTile;
    public abstract IEnumerator TriggerEvent(UnitController unit, Vector3Int location);

    public void ReplaceAestheticTile(Vector3Int location)
    {
        if(alternateTile != null)
        {
            MapTileController tile =  GameManager.Instance.levelMapManager.GetValue(location);
            tile.ChangeAestheticTile(alternateTile);
        }
        
    }
}
