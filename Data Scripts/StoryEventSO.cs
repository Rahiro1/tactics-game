using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Story/StoryEvent")]
public abstract class StoryEventSO : ScriptableObject 
{
    public abstract void TriggerEvent();
}
