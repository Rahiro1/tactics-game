using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatDisplayLevelUp : MonoBehaviour
{
    public TextMeshProUGUI statValueText, statIncreaseText;
    public WaitForSeconds waitTime = new(0.2f);

    // this method loads the stat number before the increase
    public void LoadMenu(int statAfterIncrease, int increase)
    {
        statValueText.text = (statAfterIncrease - increase).ToString();
        statIncreaseText.gameObject.SetActive(false);
    }

    public IEnumerator IncreaseStat(int statAfterIncrease, int increase)
    {
        if(increase > 0)
        {
            yield return waitTime;
            statIncreaseText.text = "+" + increase.ToString();
            statIncreaseText.gameObject.SetActive(true);
        }

        // TODO insert animation here for text changing. don't know how to do this yet
        statValueText.text = statAfterIncrease.ToString();
        yield break;
    }
}
