using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMapManager : MonoBehaviour
{
    // based on code monkey on Youtube
    // Grid code adapted from their tutorials


    private int width;
    private int height;
    private MapTileController[,] gridarray;
    private float cellsize;
    [SerializeField] private GameObject tilePrefab;


    // Load and Unload
    public void LoadMap(LevelSO level)
    {
        level.tileMap.CompressBounds();
        width = level.tileMap.size.x;
        height = level.tileMap.size.y;
        //this.originPosition = originPosition;

        gridarray = new MapTileController[width, height];

        for (int x = 0; x < gridarray.GetLength(0); x++)
        {
            for (int y = 0; y < gridarray.GetLength(1); y++)
            {
                // for clarity 
                Vector3Int mapLocation = new Vector3Int(x, y, 0);
                
                gridarray[x, y] = Instantiate(tilePrefab, GameManager.Instance.tileMapGrid.CellToWorld(mapLocation),  Quaternion.identity).gameObject.GetComponent<MapTileController>();
                gridarray[x, y].MapLocation = mapLocation; 
                // if tilemap contains a tile here
                if (level.tileMap.GetTile(mapLocation) != null)
                {
                    DataTile tileBlueprint = level.tileMap.GetTile<DataTile>(mapLocation);
                    Sprite backgroundSprite = level.tileMap.GetSprite(mapLocation);
                    Sprite aestheticSprite = level.aestheticTilemap.GetSprite(mapLocation);
                    // initialise MTC with terrain data and looks
                    gridarray[x, y].InitialiseTile(backgroundSprite, tileBlueprint.tileType, tileBlueprint.color, tileBlueprint.rotationAmount, aestheticSprite);
                }
                else
                {
                    // assign null tile
                }
            }
        }

    }

    public void UnloadMap()
    {
        for (int x = 0; x < gridarray.GetLength(0); x++)
        {
            for (int y = 0; y < gridarray.GetLength(1); y++)
            {
                if(gridarray[x,y] != null)
                {
                    if(gridarray[x,y].gameObject != null)
                    {
                        Destroy(gridarray[x, y].gameObject);
                    }
                    
                    Destroy(gridarray[x, y]);
                }
                
            }
        }
    }


    //Set and Get

    // units are set to locations to quickly check if there is a unit there in pathfinding etc.
    public void SetUnit(Vector3Int location, UnitController unit)
    {
        gridarray[location.x, location.y].SetUnit(unit);
    }

    public void RemoveUnit(Vector3Int location)
    {
        gridarray[location.x, location.y].RemoveUnit();
    }

    public UnitController GetUnit(Vector3Int location)
    {
        return gridarray[location.x, location.y].getUnit();
    }


    public MapTileController GetValue(Vector3Int location)
    {

        if (ValidLocation3(location))
        {
            return gridarray[location.x, location.y];
        }
        else
        {
            return default;
        }
    }

    // GetNeighbour Methods

    public List<MapTileController> NeighbourTilesNotObstructed(MapTileController tile, LevelMapManager grid, Define.UnitType moveType) // CONSIDER - double check if this is likely to be used in future
    {
        List<MapTileController> neighbours = new List<MapTileController>();

        CheckIfValidUnoccupiedAndAccessible(grid, new Vector3Int(tile.MapLocation.x + 1, tile.MapLocation.y), neighbours, moveType);
        CheckIfValidUnoccupiedAndAccessible(grid, new Vector3Int(tile.MapLocation.x - 1, tile.MapLocation.y), neighbours, moveType);
        CheckIfValidUnoccupiedAndAccessible(grid, new Vector3Int(tile.MapLocation.x, tile.MapLocation.y + 1), neighbours, moveType);
        CheckIfValidUnoccupiedAndAccessible(grid, new Vector3Int(tile.MapLocation.x, tile.MapLocation.y - 1), neighbours, moveType);

        return neighbours;
    }

    public List<MapTileController> NeighbourTilesIncludingFriendly(MapTileController tile, LevelMapManager grid, Define.UnitAllignment unitAllignment)
    {
        List<MapTileController> neighbours = new List<MapTileController>();

        CheckIfValidUnoccupiedOrFriendly(grid, new Vector3Int(tile.MapLocation.x + 1, tile.MapLocation.y), neighbours, unitAllignment);
        CheckIfValidUnoccupiedOrFriendly(grid, new Vector3Int(tile.MapLocation.x - 1, tile.MapLocation.y), neighbours, unitAllignment);
        CheckIfValidUnoccupiedOrFriendly(grid, new Vector3Int(tile.MapLocation.x, tile.MapLocation.y + 1), neighbours, unitAllignment);
        CheckIfValidUnoccupiedOrFriendly(grid, new Vector3Int(tile.MapLocation.x, tile.MapLocation.y - 1), neighbours, unitAllignment);

        return neighbours;
    }

    public List<MapTileController> NeighbourTilesAll(MapTileController tile, LevelMapManager grid)
    {
        List<MapTileController> neighbours = new List<MapTileController>();

        CheckIfValidAll(grid, new Vector3Int(tile.MapLocation.x + 1, tile.MapLocation.y), neighbours);
        CheckIfValidAll(grid, new Vector3Int(tile.MapLocation.x - 1, tile.MapLocation.y), neighbours);
        CheckIfValidAll(grid, new Vector3Int(tile.MapLocation.x, tile.MapLocation.y + 1), neighbours);
        CheckIfValidAll(grid, new Vector3Int(tile.MapLocation.x, tile.MapLocation.y - 1), neighbours);

        return neighbours;
    }


    // Location Validity Methods

    public bool ValidLocation3(Vector3Int location)
    {
        if (location.x >= 0 && location.x < width)
        {
            if (location.y >= 0 && location.y < height)
            {
                return true;
            }
        }

        return false;
    }

    public void CheckIfValidAll(LevelMapManager grid, Vector3Int locationToCheck, List<MapTileController> neighbours)
    {
        MapTileController tileToCheck = grid.GetValue(locationToCheck);

        if (grid.ValidLocation3(locationToCheck))
        {
            neighbours.Add(tileToCheck);
        }
    }


    public void CheckIfValidUnoccupiedAndAccessible(LevelMapManager grid, Vector3Int locationToCheck, List<MapTileController> neighbours, Define.UnitType moveType)
    {
        MapTileController tileToCheck = grid.GetValue(locationToCheck);

        if (grid.ValidLocation3(locationToCheck))
        {
            if (!tileToCheck.occupied && tileToCheck.GetMoveCost(moveType) < 100)
            {
                neighbours.Add(tileToCheck);
            }
        }
    }

    public void CheckIfValidUnoccupiedOrFriendly(LevelMapManager grid, Vector3Int locationToCheck, List<MapTileController> neighbours, Define.UnitAllignment unitAllignment)
    {
        MapTileController tileToCheck = grid.GetValue(locationToCheck);

        if (grid.ValidLocation3(locationToCheck))
        {
            if (!tileToCheck.occupied || grid.GetUnit(locationToCheck).Character.unitAllignment == unitAllignment)
            {
                neighbours.Add(tileToCheck); //CONDIDER - the add operation is taking a fair bit of time here, could restructure in some way? Or figure out how to call less
            }
        }
    }

    // Misc

    // for pathfinding and rangefinding
    public int GetManhattenDistance(MapTileController start, MapTileController end)
    {
        return Mathf.Abs(start.MapLocation.x - end.MapLocation.x) + Mathf.Abs(start.MapLocation.y - end.MapLocation.y);
    }

    public void ResetMapCumulativeMoveCost()
    {
        for (int x = 0; x < gridarray.GetLength(0); x++)
        {
            for (int y = 0; y < gridarray.GetLength(1); y++)
            {
                gridarray[x, y].InitialiseCumulativeMoveNotStartTile();

            }
        }
    }


}
