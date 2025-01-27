using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitSelectionUIManager : MonoBehaviour
{
    public PrecombatUIManager PrecombatUIManager;
    public List<CharDisplayButton> unitButtonList;
    public TextMeshProUGUI unitDeploymentInfoText;
    public GameObject container;
    public GridLayoutGroup grid;
    public int numberAcross;

    public void OpenMenu()
    {
        LoadUnitView();
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OnExitClicked()
    {
        PrecombatUIManager.OpenMenu();
        CloseMenu();
    }

    public void LoadUnitView()
    {
        float buttonSize = container.GetComponent<RectTransform>().rect.width / numberAcross;
        grid.cellSize = new Vector2(buttonSize, buttonSize);

        if(unitDeploymentInfoText != null)
        {
            RefreshDeploymentInfo();
        }

        List<int> deployedIDList = new List<int>();
        foreach(CharDisplayButton script in unitButtonList)
        {
            script.gameObject.SetActive(false);
            script.Greyout();
        }


        int count = 0;
        foreach (UnitController unit in GameManager.Instance.playerList) // TODO - ensure that count cannot exceed List size
        {
            Character character = unit.Character;
            unitButtonList[count].DisplayCharacter(character);
            unitButtonList[count].UnGreyout();
            unitButtonList[count].gameObject.SetActive(true);
            deployedIDList.Add(character.characterID);
            count++;
        }
        foreach (Character character in GameManager.Instance.playerData.PlayerCharacters) // TODO - ensure that count cannot exceed List size
        {
            // TODO - ensure this is correct was too tired 
            if (!deployedIDList.Contains(character.characterID))
            {
                unitButtonList[count].DisplayCharacter(character);
                unitButtonList[count].gameObject.SetActive(true);
                count++;
                // TODO - enable greying out of unit
            }
        }
    }

    public void RefreshDeploymentInfo()
    {
        unitDeploymentInfoText.text = GameManager.Instance.playerList.Count.ToString() + "/" + GameManager.Instance.Level.playerPositions.Count.ToString();
    }
}
