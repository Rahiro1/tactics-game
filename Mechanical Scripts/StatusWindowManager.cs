using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusWindowManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI unitNameText, levelValueText, expValueText, battationText ,battalionValueText, hpValuesText;
    [SerializeField] private Image unitImage;

    public void OpenMenu(UnitController unit, Vector3 position)
    {
        Character chr = unit.Character;

        unitNameText.text = chr.characterName;
        levelValueText.text = chr.Level.Level.ToString();
        expValueText.text = chr.Level.Experience.ToString();
        if (chr.unitAllignment != Define.UnitAllignment.Enemy)
        {
            battationText.gameObject.SetActive(false);
            battalionValueText.gameObject.SetActive(false);
        }
        else
        {
            battalionValueText.text = unit.BattalionNumber.ToString();
            battationText.gameObject.SetActive(true);
            battalionValueText.gameObject.SetActive(true);
        }
        hpValuesText.text = chr.currentHP.ToString() + "/" + chr.HP.value.ToString();

        unitImage.sprite = chr.GetCharacterSprite();

        gameObject.transform.position = position;

        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
