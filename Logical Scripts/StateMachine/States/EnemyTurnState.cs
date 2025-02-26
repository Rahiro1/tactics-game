using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : State
{
    public EnemyTurnState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {

        foreach(Battalion battalion in gameManager.battalionList)
        {
            battalion.CheckForActivation();
        }



        // cycle through enemies and implement their AI ( move and attack usually
        // uding a reverse for loop as implement AI can lead to the unit removing itself from the enemy list
        for (int i = gameManager.enemyList.Count - 1; i>=0; i--)
        {
            yield return gameManager.StartCoroutine(gameManager.enemyList[i].ImplementAI());
            gameManager.CheckForLevelEnd();
            if (gameManager.isEndState)
            {
                yield break;
            }
        }

        for (int i = gameManager.allyList.Count - 1; i >= 0; i--)
        {
            yield return gameManager.StartCoroutine(gameManager.allyList[i].ImplementAI());
            gameManager.CheckForLevelEnd();
            if (gameManager.isEndState)
            {
                yield break;
            }
        }

        for (int i = gameManager.otherList.Count - 1; i >= 0; i--)
        {
            yield return gameManager.StartCoroutine(gameManager.otherList[i].ImplementAI());
            gameManager.CheckForLevelEnd();
            if (gameManager.isEndState)
            {
                yield break;
            }
        }

        // reverse for loop 
        // could make this a function
        //foreach(UnitController unit in tempEnemyList)
        //{
        //    unit.ImplementAI();
        //}
        //Debug.Log("Completed Enemy Turn");
        gameManager.SetState(new EndEnemyTurnState(gameManager));

        yield break;
    }
}
