using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitMenuManager : AbstractUnitMenu 
{
    [SerializeField] private GameObject attackButton, itemButton, interactButton, healButton, waitButton, tradeButton;


    public void OnStatsMenuClicked()
    {
        GameManager.Instance.OpenStatsScreen(GameManager.Instance.selectedPlayer);
    }
    public void OnWaitClicked()
    {
        GameManager.Instance.OnWaitClick();
    }

    public void OnAttackClicked()
    {
        //GameManager.Instance.OnAttackClicked();
        CloseMenu();
        mainGameMenuManager.OpenMenu(Define.MenuType.InventoryMenuAttack, previousMenus, menuUnit, menuPosition);
    }

    public void OnHealClicked()
    {

        CloseMenu();
        mainGameMenuManager.OpenMenu(Define.MenuType.InventoryMenuHeal, previousMenus, menuUnit, menuPosition);
    }

    public void OnInventoryMenuClicked()
    {
        CloseMenu();
        mainGameMenuManager.OpenMenu(Define.MenuType.InventoryMenu, previousMenus, menuUnit, menuPosition);
    }

    public void OnTradeClicked()
    {
        CloseMenu();
        GameManager.Instance.SetState(new TradeState(GameManager.Instance));
    }

    public void OnInteractClicked()
    {
        GameManager gameManager = GameManager.Instance;
        gameManager.StartCoroutine(ResolveInteract());
    }

    public IEnumerator ResolveInteract()
    {
        GameManager gameManager = GameManager.Instance;
        UnitController playerSelected = gameManager.selectedPlayer;

        foreach (Define.MapEventData eventData in gameManager.Level.mapEventList)
        {
            if (playerSelected.Location == eventData.location && gameManager.mapEventsList.Contains(eventData.mapEvent))
            {
                CloseMenu();
                // CONDISER - could set the game to non interactable state during message display
                playerSelected.SetHasActed(true);
                gameManager.selectedPlayer = null;
                gameManager.mapEventsList.Remove(eventData.mapEvent);
                gameManager.SetState(new PlayerTurnState(gameManager));  // CONSIDER moving this code so that all calls to setstae are from within gamemanage
                yield return gameManager.StartCoroutine(eventData.mapEvent.TriggerEvent(playerSelected, eventData.location));
                yield break;
            }
        }
        yield break;
    }

    public override void OpenMenu(Define.MenuType menuToOpen, List<Define.MenuType> previousMenus, UnitController unit, Vector3 position)
    {
        this.previousMenus = previousMenus;
        menuUnit = unit;
        menuPosition = position;
        gameObject.transform.position = position;

        // hiding and unhiding options

        attackButton.SetActive(false);
        itemButton.SetActive(false);
        interactButton.SetActive(false);
        healButton.SetActive(false);
        tradeButton.SetActive(false);
        // wait is always active I think

        foreach(MapTileController tile in unit.GetStaticAttackRange())
        {
            if (tile.occupied)
            {
                if(tile.getUnit().Character.unitAllignment == Define.UnitAllignment.Enemy)
                {
                    attackButton.SetActive(true);
                }
            }
        }

        foreach (MapTileController tile in GameManager.Instance.levelMapManager.NeighbourTilesAll(unit.LocationTile, GameManager.Instance.levelMapManager))
        {
            if (tile.occupied)
            {
                if (tile.getUnit().Character.unitAllignment == Define.UnitAllignment.Player)
                {
                    tradeButton.SetActive(true);
                }
            }
        }

        if(unit.Character.CharacterInventory.Count != 0)
        {
            itemButton.SetActive(true);
        }

        foreach(Define.MapEventData eventData in GameManager.Instance.Level.mapEventList)
        {
            if (GameManager.Instance.mapEventsList.Contains(eventData.mapEvent) && unit.Location == eventData.location)
            {
                interactButton.SetActive(true);
            }
        }

        // TODO - potential heal range?
        foreach (Item item in unit.Character.CharacterInventory)
        {
            if(item is HealingMagic)
            {
                healButton.SetActive(true);
            }
        }

        gameObject.SetActive(true);
    }

    public override void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    public override void CloseToPreviousMenu()
    {
        gameObject.SetActive(false);
        mainGameMenuManager.CloseToPreviousMenu(menuUnit, menuPosition);
    }
}
