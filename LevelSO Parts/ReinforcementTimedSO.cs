using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ReinforcementGroup/Timed")]
public class ReinforcementTimedSO : ReinforcementSO
{
    public List<int> turnsToDeploy;
    public override void StandardActivation()
    {
        foreach (int i in turnsToDeploy)
        {
            if( GameManager.Instance.turnNumber == i) 
            {
                AddReinforcements();
            }
        }
    }
}
