using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ReinforcementSO : ScriptableObject
{
    public List<Define.GenericEnemyData> genericRList;
    // CONSIDER - adding character reinforcements

    public abstract void StandardActivation();

    public void AlternateActivation()
    {
        AddReinforcements();
    }

    protected void AddReinforcements()
    {
        
        foreach (Define.GenericEnemyData unitData in genericRList)
        {
            GameManager.Instance.AddGenericUnit(unitData);
        }
    }
}
