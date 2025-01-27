using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattalionPanel : MonoBehaviour
{
    public TextMeshProUGUI numberValueText, activationConditionText, currentAIText, activatedAIText, sizeValueText;

    public void LoadMenu(UnitController characterUnit)
    {
        Character character = characterUnit.Character;

        if(character.unitAllignment != Define.UnitAllignment.Enemy || characterUnit.battalion == null)
        {
            numberValueText.text = "--";
            activationConditionText.text = "--";
            currentAIText.text = "--";
            activatedAIText.text = "--";
            sizeValueText.text = "--";

        }
        else
        {
            string leaderString = "";

            if(characterUnit.battalion is BattalionLeaderAttackRange battalion)
            {
                if(characterUnit == battalion.LeaderUnit)
                {
                    leaderString = " (leader)";
                }
            }


            if (character.unitAllignment != Define.UnitAllignment.Player)
            {
                currentAIText.text = Define.AITypeToText(characterUnit.aIType) + leaderString;
            }

            numberValueText.text = character.battalionNumber.ToString();
            activationConditionText.text = Define.OrderTypeToText(characterUnit.battalion.battalionOrderType);
            activatedAIText.text = Define.AITypeToText(characterUnit.activatedAIType) + leaderString;
            sizeValueText.text = characterUnit.battalion.battalionUnits.Count.ToString();
        }
        
    }
}
