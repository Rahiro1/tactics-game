using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnState : State
{
    public PlayerTurnState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        gameManager.cameraMovement.EnableCameraPan();
        return base.Start();
    }

    public void RehighlightEnemyRanges()
    {
        foreach(UnitController enemy in gameManager.enemyList)
        {
            enemy.RefreshEnemyRange();
        }
    }

    public override IEnumerator CharaterStatsScreenToggle()
    {
        return base.CharaterStatsScreenToggle();
    }

    public override IEnumerator ClickEmptyTile(MapTileController tileClicked)
    {
        //gameManager.menuLocation = tileClicked.transform.position;
        //gameManager.inGameMenu.transform.position = gameManager.mainCamera.WorldToScreenPoint(gameManager.menuLocation);
        //gameManager.inGameMenu.gameObject.SetActive(true);
        gameManager.mainGameMenuManager.OpenMenu(Define.MenuType.InGameMenu, new List<Define.MenuType>(), gameManager.selectedPlayer, tileClicked.transform.position);
        gameManager.SetState(new MenuState(gameManager));
        yield break;
        // TODO implement playerTurn gameMenu acccess
    }

    public override IEnumerator ClickEnemy(UnitController unit)
    {
        // possibly move this to unit controller script as a method

        if (unit.enemyRangeHighlighted == null)
        {
            unit.HighlightEnemyRange();
        } else
        {
            unit.UnHighlightEnemyRange();
        }

        yield break;
    }

    public override IEnumerator ClickPlayer(UnitController unit)
    {
        if (unit.hasActed)
        {
            yield break;
        }
        gameManager.selectedPlayer = unit;
        gameManager.SetState(new SelectedState(gameManager));
        yield break;
    }

    public override IEnumerator RightClickUnit(UnitController unit)
    {
        gameManager.OpenStatsScreen(unit);
        return base.RightClickUnit(unit);
    }

}
