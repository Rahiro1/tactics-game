using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Assets/Tile Type")]
public class MapTileSO : ScriptableObject
{
    // TODO rename this class as the name is confusing

    public Define.TileType TileType;
    // move costs for different move types
    public int footMoveCost;
    public int armourMoveCost;
    public int waterMoveCost;
    public int flyMoveCost;
    public int beastMoveCost;

    // terrain bonuses
    public int bonusAvoid, bonusGuard, bonusRegeneration;
}
