using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// tutorial used is by lawless games on youtube - fixed bug with lawless games code where initial aStarG was not initialised to 0 - only relevent after terrain added
// fixed second bug where the metrcis need to be set to the minimum value of the new and old value when the tile is looked at each time
// changed fuction to not include the list sort Linq function

public class Pathfinder
{
    private LevelMapManager grid;
    private MapTileController tempTile;

    public Pathfinder(LevelMapManager grid)
    {
        this.grid = grid;
    }
    public List<MapTileController> Pathfind(MapTileController start, MapTileController end, Define.UnitType movePower, Define.UnitAllignment unitAllignment, int movement)
    {
        List<MapTileController> openList = new List<MapTileController>();
        List<MapTileController> closedList = new List<MapTileController>();
        List<MapTileController> neighbours;
        MapTileController currentTile;
        int aStarGTemp;

        openList.Add(start);
        // can be non-zero from previous pathfinding
        start.aStarG = 0;

        if (end.occupied)
        {
            return new List<MapTileController>();
        }

        while (openList.Count > 0)
        {
            //MapTileController currentTile = openList.OrderBy(x => x.aStarF).First();  // replace this with normal loop?

            currentTile = FindHighestFTile(openList);

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            neighbours = grid.NeighbourTilesIncludingFriendly(currentTile, grid, unitAllignment);

            if (currentTile == end)
            { 
                return GetFinishedPath(start, end); 
            }

            foreach (MapTileController tile in neighbours)
            {
                
                if (closedList.Contains(tile))
                {
                    continue;
                }
                if (!openList.Contains(tile))
                {
                    // if the tile has not been checked before initialise aStarG so that it is always overwritten
                    tile.aStarG = int.MaxValue;
                }

                aStarGTemp = currentTile.aStarG + tile.GetMoveCost(movePower);

                // if new path is shorter set shortest path as using the current tile
                if(aStarGTemp < tile.aStarG)
                {
                    tile.Previous = currentTile;
                }

                tile.aStarG = Mathf.Min(tile.aStarG, aStarGTemp);
                tile.aStarH = Mathf.Min(tile.aStarH,grid.GetManhattenDistance(tile, end)); // need to change this?
                tile.aStarF = Mathf.Min(tile.aStarF,tile.aStarG + tile.aStarH);
                
                // CONSIDER - removing the adding to closed list part?
                if (!openList.Contains(tile) && tile.aStarG <= movement)
                {
                    openList.Add(tile);
                } else if (tile.aStarG >= movement)
                {
                    closedList.Add(tile);
                }
            }

        }

        return new List<MapTileController>();
    }

    private MapTileController FindHighestFTile(List<MapTileController> openList)
    {
        int aStarFTemp = -1000;

        foreach (MapTileController tile in openList)
        {
            if (tile.aStarF > aStarFTemp)
            {
                tempTile = tile;
                aStarFTemp = tile.aStarF;
            }
        }

        return tempTile;
    }

    private List<MapTileController> GetFinishedPath(MapTileController start, MapTileController end)
    {
        List<MapTileController> path = new List<MapTileController>();
        MapTileController currentTile = end;

        while (currentTile != start)
        {
            path.Add(currentTile);
            currentTile = currentTile.Previous;
        }

        path.Reverse();

        return path;

    }
}
