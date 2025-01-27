using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsPanel : MonoBehaviour
{
    
    public List<SkillIcon> skillIcons;

    public void LoadMenu(List<SkillSO> skillList)
    {
        foreach (SkillIcon skillIcon in skillIcons)
        {
            skillIcon.CloseMenu();
        }

        int count = 0;
        foreach (SkillSO skill in skillList)
        {
            if (count >= skillIcons.Count)
            {
                continue;
            }

            skillIcons[count].OpenMenu(skill);
            count++;
        }
    }
}
