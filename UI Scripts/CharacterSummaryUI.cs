using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSummaryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterNameText, classText, levelValueText, experienceValueText, movementTypeValueText, allignmentValueText, hPValueText, armourValueText;
    [SerializeField] private Image characterSprite;
    [SerializeField] private List<Image> armourIcons;

    public void LoadMenu(Character character)
    {
        characterNameText.text = character.characterName;
        classText.text = character.GetClassSO().className;
        levelValueText.text = character.Level.Level.ToString();
        experienceValueText.text = character.Level.Experience.ToString();
        hPValueText.text = character.currentHP.ToString();
        armourValueText.text = character.currentArmour.ToString();
        movementTypeValueText.text = Define.UnitTypeToText(character.unitType);
        allignmentValueText.text = Define.UnitAllignmentToText(character.unitAllignment);
        LoadArmourTypeDisplay(character.GetClassSO().allowedArmourTypes);
        characterSprite.sprite = character.GetCharacterSprite();
    }

    public void LoadArmourTypeDisplay(List<Define.ArmourType> allowedArmourList)
    {
        foreach(Image armourIcon in armourIcons)
        {
            armourIcon.gameObject.SetActive(false);
        }

        int count = 0;
        foreach(Define.ArmourType armourType in allowedArmourList)
        {
            if(count >= armourIcons.Count)
            {
                continue;
            }

            armourIcons[count].sprite = Database.Instance.genericArmourIconDictionary[armourType];
            armourIcons[count].gameObject.SetActive(true);
            count++;
        }
    }
    
}
