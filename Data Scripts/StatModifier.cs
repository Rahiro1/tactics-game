using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier 
{
    public string source;
    public int modifier;

    public StatModifier(string source, int modifier)
    {
        this.source = source;
        this.modifier = modifier;
    }
}
