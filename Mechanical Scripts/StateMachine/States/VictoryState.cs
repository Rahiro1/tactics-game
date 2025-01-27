using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : State
{
    public VictoryState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        // display victory screen
        // TODO - add gaining bonusXP and gold??? here
        EndOfChapterUpkeep();
        // display after batlle statistics 
        // change game manager to next level
        gameManager.EndLevel();

        yield break;
    }

    public void DisplayVictoryScreen()
    {

    }
    
    // not sure exactly what the name should be
    // covers gaining bonusXP and other rewards at end of chapter
    public void EndOfChapterUpkeep()
    {
        foreach(BonusConditionSO bonus in gameManager.Level.bonusConditionsList)
        {
            gameManager.playerData.PlayerBonusXP += bonus.BonusTimeCurrent();
            bonus.GainOtherRewards();
        }
    }
}
