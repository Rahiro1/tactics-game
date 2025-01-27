using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PCUIMainMenuManager : MonoBehaviour
{
    [SerializeField] private Image speech;
    [SerializeField] private Button unitSelectionButton, viewMapButton, endButton, mapReturnButton;
    [SerializeField] private PrecombatUIManager precombatUIManager;
    [SerializeField] private UnitSelectionUIManager unitSelectionUIManager;
    [SerializeField] private PCOverallInventoryPanelManager inventoryManager;
    [SerializeField] private PCStoresPanelManager shopManager;

    public void OpenMenu()
    {
        speech.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        speech.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void OnExitClicked()
    {
        CloseMenu();
        precombatUIManager.CloseMenu();
    }

    public void OnUnitSelectClicked()
    {
        CloseMenu();
        unitSelectionUIManager.OpenMenu();
    }

    public void OnOverallInventoryClicked()
    {
        CloseMenu();
        inventoryManager.OpenMenu();
    }

    public void OnShopClicked()
    {
        CloseMenu();
        shopManager.OpenMenu();
    }

    public void OnViewMapClicked()
    {
        CloseMenu();
        mapReturnButton.gameObject.SetActive(true);
        GameManager.Instance.SetState(new PrecombatMapState(GameManager.Instance));
    }

    public void OnMapStateReturnClicked()
    {
        GameManager gameManager = GameManager.Instance;
        mapReturnButton.gameObject.SetActive(false);
        foreach (Vector3Int location in gameManager.Level.playerPositions)  // TODO - feels like this should be in an exit metohd of PreCombatMapState
        {
            gameManager.levelMapManager.GetValue(location).UnHighlight();
        }
        gameManager.SetState(new PreCombatState(gameManager));
    }
}
