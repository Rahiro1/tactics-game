using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryConditionDisplay : MonoBehaviour
{
    public List<VictoryConditionDisplayImage> conditionDisplayImages;

    public void LoadConditions(List<WinConditionSO> conditionList)
    {
        foreach(VictoryConditionDisplayImage displayImage in conditionDisplayImages)
        {
            displayImage.Hide();
        }


        int count = 0;
        foreach(WinConditionSO condition in conditionList)
        {
            conditionDisplayImages[count].DisplayVictoryCondition(condition);
            count++;
        }
    }
}
