using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statValueText;
    [SerializeField] private Slider slider;

    public void LoadSlider(int unmodifiedStat, int modifiedStat)
    {
        slider.maxValue = 30;
        slider.value = modifiedStat;

        statValueText.text = modifiedStat.ToString();
        if(modifiedStat > unmodifiedStat)
        {
            statValueText.color = Color.green;
        }
        if(modifiedStat < unmodifiedStat)
        {
            statValueText.color = Color.red;
        }
        if(modifiedStat == unmodifiedStat)
        {
            statValueText.color = Color.white;
        }
    }
}
