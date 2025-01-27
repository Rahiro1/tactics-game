using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour, ITooltipBasicSupplier
{
    public SkillSO skill;
    public Image skillImage;

    public void OpenMenu(SkillSO skill)
    {
        this.skill = skill;
        skillImage.sprite = skill.skillIcon;
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public string TooltipContent()
    {
        return skill.description;
    }

    public string TooltipHeader()
    {
        return skill.skillName;
    }
}
