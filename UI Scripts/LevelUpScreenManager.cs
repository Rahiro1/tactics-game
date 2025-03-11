using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpScreenManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitNameText;
    [SerializeField] private TextMeshProUGUI levelValueText;
    [SerializeField] private StatDisplayLevelUp hpDisplay, strDisplay, magDisplay, offDisplay, defDisplay, resDisplay, spdDisplay;

    public IEnumerator OpenMenu(Character unit, int hpIncrease, int strIncrease, int magIncrease, int offIncrease, int defIncrease, int resIncrease, int spdIncrease)
    {
        unitNameText.text = unit.characterName;
        levelValueText.text = unit.Level.Level.ToString();

        // load stats initial numbers (before increase)
        hpDisplay.LoadMenu(unit.MaxHP.GetbaseValue(), hpIncrease);
        strDisplay.LoadMenu(unit.Strength.GetbaseValue(), strIncrease);
        magDisplay.LoadMenu(unit.Magic.GetbaseValue(), magIncrease);
        offDisplay.LoadMenu(unit.Offence.GetbaseValue(), offIncrease);
        defDisplay.LoadMenu(unit.Defence.GetbaseValue(), defIncrease);
        resDisplay.LoadMenu(unit.Resistance.GetbaseValue(), resIncrease);
        spdDisplay.LoadMenu(unit.Speed.GetbaseValue(), spdIncrease);

        gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(hpDisplay.IncreaseStat(unit.MaxHP.GetbaseValue(), hpIncrease));
        yield return StartCoroutine(strDisplay.IncreaseStat(unit.Strength.GetbaseValue(), strIncrease));
        yield return StartCoroutine(magDisplay.IncreaseStat(unit.Magic.GetbaseValue(), magIncrease));
        yield return StartCoroutine(offDisplay.IncreaseStat(unit.Offence.GetbaseValue(), offIncrease));
        yield return StartCoroutine(defDisplay.IncreaseStat(unit.Defence.GetbaseValue(), defIncrease));
        yield return StartCoroutine(resDisplay.IncreaseStat(unit.Resistance.GetbaseValue(), resIncrease));
        yield return StartCoroutine(spdDisplay.IncreaseStat(unit.Speed.GetbaseValue(), spdIncrease));

        yield break;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
