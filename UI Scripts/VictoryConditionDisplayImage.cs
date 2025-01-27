using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryConditionDisplayImage : MonoBehaviour, ITooltipBasicSupplier
{
    public TextMeshProUGUI nameText, progressText;
    public WinConditionSO conditionSO;
    public void DisplayVictoryCondition(WinConditionSO condition)
    {
        conditionSO = condition;
        nameText.text = condition.displayName;

        // check if condition has a target amount or a remaining counter
        if(condition.ProgressTarget() != -1)
        {
            progressText.text = condition.ProgressAmount().ToString() + "/" + condition.ProgressTarget().ToString();
        }
        else
        {
            progressText.text = condition.ProgressAmount().ToString();
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public string TooltipContent()
    {
        return conditionSO.description;
    }

    public string TooltipHeader()
    {
        return conditionSO.displayName;
    }
}
