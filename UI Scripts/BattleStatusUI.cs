using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatusUI : MonoBehaviour
{
    public VictoryConditionDisplay playerVictoryConditionDisplay;
    public VictoryConditionDisplay bonusConditionDisplay;
    public VictoryConditionDisplay playerDefeatConditionDisplay;
    public ForceSummaryPanel playerForceSummary;
    public ForceSummaryPanel enemyForceSummary;
    public ForceSummaryPanel otherForceSummary;
    public ForceSummaryPanel allyForceSummary;

    public void OpenMenu()
    {
        GameManager gameManager = GameManager.Instance;
        List<WinConditionSO> bonusConditionsList = new List<WinConditionSO>();      // I wonder if there is a better way to do this - this is because I can't directly use a list of child objects as it is by reference -> you could edit original list as it it wee a list of parent objects
        bonusConditionsList.AddRange(gameManager.Level.bonusConditionsList);

        playerVictoryConditionDisplay.LoadConditions(gameManager.Level.victoryConditionsList);
        bonusConditionDisplay.LoadConditions(bonusConditionsList);
        playerDefeatConditionDisplay.LoadConditions(gameManager.Level.defeatConditionsList);

        playerForceSummary.OpenMenu(Database.Instance.CharacterDictionary[0], gameManager.playerList.Count);        // TODO - display NPC commanders appropriately
        enemyForceSummary.OpenMenu(Database.Instance.CharacterDictionary[0], gameManager.enemyList.Count);
        otherForceSummary.OpenMenu(Database.Instance.CharacterDictionary[0], gameManager.otherList.Count);
        allyForceSummary.OpenMenu(Database.Instance.CharacterDictionary[0], gameManager.allyList.Count);

        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OnExitClicked()
    {
        CloseMenu();
    }
}
