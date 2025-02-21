using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        // TODO highlight tiles adjacent to player
        gameManager.inGameUnitMenu.gameObject.SetActive(false);
        HighlightAttackRange();
        yield break;
    }

    public void HighlightAttackRange()
    {
        foreach (MapTileController tile in gameManager.selectedPlayer.GetEquippedAttackRange())
        {
            tile.Highlight(); // CONDIDER change the highlight, when this happens also change the exit method
        }
    }

    public override IEnumerator ClickEnemy(UnitController unit)
    {
        // check player attack range
        //Debug.Log("Click Enemy Attack State");
        UnitController attacker = gameManager.selectedPlayer;
          
        if(gameManager.levelMapManager.GetManhattenDistance(attacker.LocationTile, unit.LocationTile) <= attacker.Character.EquippedWeapon.range)
        {
            OnExitingState();
            gameManager.battleForcast.OpenMenu(attacker, unit);
        }
        yield break;
    }

    public override IEnumerator Cancel()
    {
        if (gameManager.battleForcast.isActiveAndEnabled)
        {
            gameManager.battleForcast.CloseMenu();
            HighlightAttackRange();
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
        foreach (MapTileController tile in gameManager.selectedPlayer.GetStaticAttackRange())
        {
            tile.UnHighlight();
        }
    }


    // TODO implement cancel method




} 
