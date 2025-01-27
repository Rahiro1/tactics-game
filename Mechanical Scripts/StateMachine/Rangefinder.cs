using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Rangefinder
{

    private LevelMapManager grid;

    public Rangefinder(LevelMapManager grid)
    {
        this.grid = grid;
    }
    public List<MapTileController> GetTilesInAttackRange(int attackRange, MapTileController start)
    {
        List<MapTileController> tilesInRange = new List<MapTileController>();
        List<MapTileController> tilesInPrevious = new List<MapTileController>();
        //List<MapTileController> tilesToRemove = new List<MapTileController>();
        int currentStep = 1;

        tilesInRange.Add(start);
        tilesInPrevious.Add(start);

        while (currentStep <= attackRange)
        {
            List<MapTileController> neighbours = new List<MapTileController>();

            foreach (MapTileController tile in tilesInPrevious)
            {
                neighbours.AddRange(grid.NeighbourTilesAll(tile, grid));
            }

            tilesInRange.AddRange(neighbours);
            tilesInPrevious = neighbours.Distinct().ToList();

            currentStep += 1;
        }

        /*foreach (MapTileController tile in tilesInRange)
        {
            if (tile.occupied)
            {
                tilesToRemove.Add(tile);
            }
        }

        foreach (MapTileController tile in tilesToRemove)
        {
            tilesInRange.Remove(tile);
        }
        */

        return tilesInRange.Distinct().ToList();
    }

    public List<MapTileController> GetTilesInRangeMoveCost(int moveRange, MapTileController start, Define.UnitType moveType, Define.UnitAllignment unitAllignment)
    {
        List<MapTileController> tilesInRange = new List<MapTileController>();
        List<MapTileController> tilesInPrevious = new List<MapTileController>();
        List<MapTileController> tilesToRemove = new List<MapTileController>();
        int currentStep = 1;

        grid.ResetMapCumulativeMoveCost(); // sets all move costs to 100
        start.InitialiseCumulativeMoveStartTile();

        tilesInRange.Add(start);
        tilesInPrevious.Add(start);

        while (currentStep <= moveRange)
        {
            List<MapTileController> totalNeighboursList = new List<MapTileController>();
            List<MapTileController> neighbours = new List<MapTileController>();

            foreach (MapTileController tile in tilesInPrevious)
            {
                neighbours = grid.NeighbourTilesIncludingFriendly(tile, grid, unitAllignment);
                totalNeighboursList.AddRange(neighbours);
                foreach(MapTileController neighbourTile in neighbours)
                {
                    neighbourTile.CalcNewCumulativeMoveCost(tile.cumulativeMoveCost, moveType);
                }
            }

            tilesInRange.AddRange(totalNeighboursList);
            tilesInPrevious = totalNeighboursList.Distinct().ToList();

            currentStep += 1;
        }

        tilesInRange.Distinct().ToList();


        foreach (MapTileController tile in tilesInRange)
        {
            if (tile.cumulativeMoveCost > moveRange|| tile.occupied)
            {
                tilesToRemove.Add(tile);
            }
        }

        foreach (MapTileController tile in tilesToRemove)
        {
            tilesInRange.Remove(tile);
        }

        tilesInRange.Remove(start);

        return tilesInRange.Distinct().ToList();
    }

    public List<MapTileController> GetTilesInAttackRangeFromMove(List<MapTileController> tilesInMoveRange, int attackRange)
    {
        if (attackRange == 0)                                             // exit immediately if no weapon to check for
        {
            return new List<MapTileController>(); // CONSIDER - should this return tilesInMoveRange?
        }

        List<MapTileController> tilesInRange = new List<MapTileController>();;
        List<MapTileController> tilesInPrevious;

        foreach (MapTileController tile in tilesInMoveRange)             // get first set of neighbours
        {
            tilesInRange.AddRange(grid.NeighbourTilesAll(tile, grid));
        }


        tilesInRange.Distinct().ToList();
        tilesInRange = tilesInRange.Except(tilesInMoveRange).ToList();
        tilesInPrevious = tilesInRange;

        int count = 1;

        while( count < attackRange)
        {
            List<MapTileController> neighbours = new List<MapTileController>();

            foreach (MapTileController tile in tilesInPrevious)
            {
                neighbours.AddRange(grid.NeighbourTilesAll(tile, grid));
            }

            neighbours.Distinct().ToList();
            tilesInPrevious = neighbours.Except(tilesInRange).ToList();
            tilesInPrevious = tilesInPrevious.Except(tilesInMoveRange).ToList();
            tilesInRange.AddRange(tilesInPrevious);

            count++;
        }

        return tilesInRange;
    }

    public List<MapTileController> GetTilesInMoveAndAttackRange(int moveRange,int attackRange, MapTileController start, Define.UnitType moveType, Define.UnitAllignment unitAllignment)
    {
        List<MapTileController> moveRangeTiles = GetTilesInRangeMoveCost(moveRange, start, moveType, unitAllignment);

        moveRangeTiles.AddRange(GetTilesInAttackRangeFromMove(moveRangeTiles, attackRange));
        return moveRangeTiles;
    }
}
