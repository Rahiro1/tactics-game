using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharDisplayButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image characterImage, weaponImage, greyoutImage;
    public Character assignedCharacter;
    public UnitSelectionUIManager unitSelectionUIManager;

    public void DisplayCharacter(Character character)
    {
        nameText.text = character.characterName;
        characterImage.sprite = character.GetCharacterSprite();
        weaponImage.sprite = character.GetClassSO().genericClassSprite; // TODO - update this to a class icon
        assignedCharacter = character;
    }

    public void OnClick()
    {
        GameManager gameManager = GameManager.Instance;
        UnitController deployedUnit = null;
        int deployLimit = gameManager.Level.playerPositions.Count;

        foreach(UnitController unit in gameManager.playerList)
        {
            if(unit.Character == assignedCharacter)
            {
                deployedUnit = unit;
            }
        }

        if (deployedUnit != null)
        {
            gameManager.RemoveUnit(deployedUnit, false);
            Greyout();
        }
        else if(gameManager.playerList.Count < deployLimit)
        {
            foreach(Vector3Int location in gameManager.Level.playerPositions)
            {
                if (!gameManager.levelMapManager.GetValue(location).occupied)
                {
                    gameManager.AddCharacterUnit(assignedCharacter, location);
                    break;
                }
            }
            UnGreyout();
        }
        unitSelectionUIManager.RefreshDeploymentInfo();
    }

    public void Greyout()
    {
        greyoutImage.gameObject.SetActive(true);
    }

    public void UnGreyout()
    {
        greyoutImage.gameObject.SetActive(false);
    }
}
