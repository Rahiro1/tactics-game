using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdvancedStatsDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI wieldValueText, attackValueText, offensiveHitValueText, offensiveAvoidValueText, guardValueText, defensiveHitValueText, criticalValueText, rendingValueText, criticalAvoidValueText;

    public void LoadMenu(Character character)
    {
        wieldValueText.text = character.Wield.ToString();
        attackValueText.text = character.Attack.ToString();
        offensiveHitValueText.text = character.OffensiveHit.ToString();
        offensiveAvoidValueText.text = character.Avoid.ToString();
        guardValueText.text = character.Guard.ToString();
        defensiveHitValueText.text = character.DefensiveHit.ToString();
        criticalValueText.text = character.CriticalRate.ToString();
        rendingValueText.text = character.Rending.ToString();
        criticalAvoidValueText.text = character.CriticalAvoid.ToString();
    }
}
