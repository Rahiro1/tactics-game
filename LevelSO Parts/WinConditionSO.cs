using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinConditionSO : ScriptableObject
{
    public string displayName;
    public string description;
    public abstract bool CheckForVictory();
    public abstract int ProgressAmount();
    public abstract int ProgressTarget();
}

