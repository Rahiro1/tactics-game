using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndEnemyTurnState : State
{
    public EndEnemyTurnState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        // TODO implement enemy end turn
        // TODO implement player upkeep

        EnemyUpkeep(); // CONSIDER - ??? surely this should be enemy end turn maintainance 
        PlayerUpkeep();

        gameManager.turnNumber += 1;

        gameManager.SetState(new PlayerTurnState(gameManager));

        return base.Start();
    }

    public void PlayerUpkeep()
    {
        foreach(UnitController unit in gameManager.playerList)
        {
            unit.StartOfTurnReset();
        }
    }

    public void EnemyUpkeep()
    {
        foreach (UnitController unit in gameManager.enemyList)
        {
            unit.StartOfTurnReset();
        }


        // TODO - randomised reinforcements will be implemented here
        foreach(ReinforcementSO reinforcementSO in gameManager.Level.reinforcmentList)
        {
            reinforcementSO.StandardActivation();
        }
    }

}
