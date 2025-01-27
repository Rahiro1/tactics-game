using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecombatMapState : State
{
    public PrecombatMapState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Cancel()
    {
        gameManager.selectedPlayer = null;
        yield break;
    }

    public override IEnumerator ClickPlayer(UnitController unit)
    {
        if(gameManager.selectedPlayer == null)
        {
            gameManager.selectedPlayer = unit;
        }
        else
        {
            // swap the two chars locations - doing this via removing and adding but might have to do bespoke instead if animation added
            Character characterSwapThis = unit.Character;
            Vector3Int locationSwapThis = unit.Location;
            Character characterSwapSelected = gameManager.selectedPlayer.Character;
            Vector3Int locationSwapSelected = gameManager.selectedPlayer.Location;



            gameManager.RemoveUnit(unit, false);
            gameManager.RemoveUnit(gameManager.selectedPlayer, false);
            gameManager.AddCharacterUnit(characterSwapThis, locationSwapSelected);
            gameManager.AddCharacterUnit(characterSwapSelected, locationSwapThis);
        }

        return base.ClickPlayer(unit);
    }

    public override IEnumerator Start()
    {
        foreach(Vector3Int location in gameManager.Level.playerPositions)
        {
            gameManager.levelMapManager.GetValue(location).Highlight();
        }
        gameManager.selectedPlayer = null;
        yield break;
    }

    public override IEnumerator RightClickUnit(UnitController unit)
    {
        gameManager.OpenStatsScreen(unit);
        return base.RightClickUnit(unit);
    }

    public override IEnumerator ClickEmptyTile(MapTileController tileClicked)
    {
        foreach(Vector3Int location in gameManager.Level.playerPositions)
        {
            if(tileClicked.MapLocation == location)
            {
                if(gameManager.selectedPlayer != null)
                {
                    Character characterSwapSelected = gameManager.selectedPlayer.Character;

                    gameManager.RemoveUnit(gameManager.selectedPlayer, false);
                    gameManager.AddCharacterUnit(characterSwapSelected, location);
                    yield break;
                }
            }
        }

        yield break;
    }



    // for exiting state code is currently in PCUIMainMenuManager it should be moved here somehow

}
