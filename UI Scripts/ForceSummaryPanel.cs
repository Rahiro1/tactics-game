using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ForceSummaryPanel : MonoBehaviour
{
    public Image commanderPortrait;
    public TextMeshProUGUI commanderNameText, remainingUnitsValueText;
    private CharacterSO commanderCharacter; // for tooltip 

    public void OpenMenu(CharacterSO character, int remainingUnits)
    {
        commanderCharacter = character;
        commanderPortrait.sprite = commanderCharacter.characterSprite;
        commanderNameText.text = character.characterName;
        remainingUnitsValueText.text = remainingUnits.ToString();
    }
}
