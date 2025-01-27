using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlayerTurnState : State
{
    public EndPlayerTurnState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        // TODO implement end of player turn
        gameManager.cameraMovement.DisableCameraPan();


        // TODO implement enemy upkeep
        foreach(UnitController unit in gameManager.enemyList)
        {
            foreach(SkillSO skill in unit.Character.GetSkillList())
            {
                unit.RecieveHeal(skill.RegenerationAmount(unit.Character), skill.RegenerationAmount(unit.Character));
            }
        }

        gameManager.SetState(new EnemyTurnState(gameManager));
        yield break;
    }
}
