using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreCombatState : State
{
    public PreCombatState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        if (!gameManager.Level.hasPrecombatPhase)
        {
            gameManager.SetState(new PlayerTurnState(gameManager));
            yield break;
        }
        else
        {
            gameManager.precombatUIManager.OpenMenu();
            gameManager.cameraMovement.EnableCameraPan();

            yield break;
        }
    }
}
