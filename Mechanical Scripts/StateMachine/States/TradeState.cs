using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeState : State
{
    private List<MapTileController> targetTiles;
    private MapTileController sourceTile;

    public TradeState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        // TODO highlight tiles adjacent to player
        gameManager.inGameUnitMenu.gameObject.SetActive(false);
        targetTiles = new List<MapTileController>();
        sourceTile = gameManager.selectedPlayer.LocationTile;

        foreach (UnitController unit in gameManager.playerList)
        {
            if (gameManager.levelMapManager.GetManhattenDistance(sourceTile, unit.LocationTile) == 1)
            {
                targetTiles.Add(unit.LocationTile);
            }
        }
        HighlightRange();
        yield break;
    }

    public void HighlightRange()
    {
        foreach (MapTileController tile in targetTiles)
        {
            tile.Highlight(); // CONDIDER change the highlight, when this happens also change the exit method
        }
    }

    public override IEnumerator ClickPlayer(UnitController unit)
    {
        // check player attack range
        //Debug.Log("Click Enemy Attack State");
        UnitController source = gameManager.selectedPlayer;

        if (gameManager.levelMapManager.GetManhattenDistance(source.LocationTile, unit.LocationTile) == 1)
        {
            OnExitingState();
            gameManager.tradeMenuManager.OpenMenu(source, unit);
        }
        yield break;
    }

    public override IEnumerator Cancel()
    {
        if (gameManager.tradeMenuManager.isActiveAndEnabled)
        {
            gameManager.tradeMenuManager.CloseMenu();
            HighlightRange();
        }
        else
        {
            OnExitingState();
            gameManager.SetState(new MenuState(gameManager));
            gameManager.mainGameMenuManager.ShowMenu();
        }
        return base.Cancel();
    }

    public void OnExitingState()
    {
        foreach (MapTileController tile in targetTiles)
        {
            tile.UnHighlight();
        }
    }
}
