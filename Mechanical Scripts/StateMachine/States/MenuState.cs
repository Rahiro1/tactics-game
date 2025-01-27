using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : State
{
    public MenuState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        return base.Start();
    }

    //public override IEnumerator Cancel()
    //{
    //    gameManager.selectedPlayer.DeselectRange();
    //    gameManager.SetState(new PlayerTurnState(gameManager));
    //   yield break;
    //}

    public override IEnumerator Cancel()
    {
        gameManager.mainGameMenuManager.OnCancelClicked();
        yield break;
    }

    public override IEnumerator ClickWait()
    {
        gameManager.selectedPlayer.SetHasActed(true);
        gameManager.selectedPlayer = null;
        gameManager.inGameUnitMenu.gameObject.SetActive(false);
        gameManager.SetState(new PlayerTurnState(gameManager));
        // CONSIDER on wait function for end of unit action effects here OR a call to UnitController to wait
        return base.ClickWait();
    }

    public override IEnumerator EndTurn()
    {
        gameManager.inGameMenu.gameObject.SetActive(false);
        gameManager.SetState(new EndPlayerTurnState(gameManager));
        yield break;
    }

    public override IEnumerator ExitMenus()
    {
        UnitController unit = gameManager.selectedPlayer;
        if (unit != null) // reseting unit to how it was before it was selected
        {

            unit.ResetToStartOfTurn(); 
        }

        gameManager.SetState(new PlayerTurnState(gameManager));
        yield break;
    }
}
