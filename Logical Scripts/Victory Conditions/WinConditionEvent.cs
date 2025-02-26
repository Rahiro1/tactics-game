using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/LevelEnd/Victory/ByEvent")]
public class WinConditionEvent : WinConditionSO
{
    // doesn't work for multi sieze at the moment
    public override bool CheckForVictory()
    {
        return false;
    }

    public override int ProgressAmount()
    {
        return 0;
    }

    public override int ProgressTarget()
    {
        return 1;
    }
}
