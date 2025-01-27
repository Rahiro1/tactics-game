using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Data Tile", menuName = "2D/Tiles/Data Tile")]
public class DataTile : Tile
{
    public Define.TileType tileType;
    public float rotationAmount = 0;
}  