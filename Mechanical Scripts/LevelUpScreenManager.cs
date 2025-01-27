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
        hpDisplay.LoadMenu(unit.HP.value, hpIncrease);
        strDisplay.LoadMenu(unit.UnmodifiedStrength.value, strIncrease);
        magDisplay.LoadMenu(unit.UnmodifiedMagic.value, magIncrease);
        offDisplay.LoadMenu(unit.UnmodifiedOffence.value, offIncrease);
        defDisplay.LoadMenu(unit.UnmodifiedDefence.value, defIncrease);
        resDisplay.LoadMenu(unit.UnmodifiedResistance.value, resIncrease);
        spdDisplay.LoadMenu(unit.UnmodifiedSpeed.value, spdIncrease);

        gameObject.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        yield return StartCoroutine(hpDisplay.IncreaseStat(unit.HP.value, hpIncrease));
        yield return StartCoroutine(strDisplay.IncreaseStat(unit.UnmodifiedStrength.value, strIncrease));
        yield return StartCoroutine(magDisplay.IncreaseStat(unit.UnmodifiedMagic.value, magIncrease));
        yield return StartCoroutine(offDisplay.IncreaseStat(unit.UnmodifiedOffence.value, offIncrease));
        yield return StartCoroutine(defDisplay.IncreaseStat(unit.UnmodifiedDefence.value, defIncrease));
        yield return StartCoroutine(resDisplay.IncreaseStat(unit.UnmodifiedResistance.value, resIncrease));
        yield return StartCoroutine(spdDisplay.IncreaseStat(unit.UnmodifiedSpeed.value, spdIncrease));

        yield break;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
