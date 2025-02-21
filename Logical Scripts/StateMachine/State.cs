using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    protected GameManager gameManager;

    public State(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual IEnumerator LeftClickGeneral()
    {
        yield break;
    }

    public virtual IEnumerator ClickPlayer(UnitController unit)
    {
        yield break;
    }

    public virtual IEnumerator ClickEnemy(UnitController unit)
    {
        yield break;
    }

    public virtual IEnumerator RightClickUnit(UnitController unit)
    {
        yield break;
    }

    public virtual IEnumerator ClickEmptyTile(MapTileController tileClicked)
    {
        yield break;
    }

    public virtual IEnumerator ClickWait()
    {
        yield break;
    }

    public virtual IEnumerator ClickNull()
    {
        yield break;
    }

    public virtual IEnumerator Cancel()
    {
        yield break;
    }

    public virtual IEnumerator SkipButton()
    {
        yield break;
    }

    public virtual IEnumerator CharaterStatsScreenToggle()
    {
        yield break;
    }

    public virtual IEnumerator EndTurn()
    {
        yield break;
    }

    public virtual IEnumerator ExitMenus()
    {
        yield break;
    }

    public virtual void OnExitState()
    {
        
    }

}
