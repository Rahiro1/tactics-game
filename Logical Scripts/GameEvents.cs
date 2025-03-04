using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }

    public event Action<Define.SkillTriggerType> onTriggerSkill;

    public void TriggerSkills(Define.SkillTriggerType skillTriggerType)
    {
        if (onTriggerSkill != null)
        {
            onTriggerSkill(skillTriggerType);
        }
    }
}
