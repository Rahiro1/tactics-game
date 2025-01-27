using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectedState : State
{
    public SelectedState(GameManager gameManager) : base(gameManager)
    {
    }

    public override IEnumerator Start()
    {
        UnitController unit = gameManager.selectedPlayer;


        unit.DisplayRange();
        // a call to the units playercontroller to display it's abilities
        // CONSIDER this should be a method for updating the unit display at the bottom left if there is one  combatController.battleCanvas.statsScreen.DisplayStatsScreen(unit);
        // what does this mean, me???

        yield break;
    }

    public override IEnumerator ClickPlayer(UnitController unit)
    {
        if (unit == gameManager.selectedPlayer)
        {
            // TOD change this code, it is very messy in structure and repeated code

            List<MapTileController> tempPath = new List<MapTileController>();
            MapTileController tileClicked = gameManager.levelMapManager.GetValue(gameManager.selectedPlayer.Location);
            tempPath.Add(tileClicked);

            gameManager.selectedPlayer.SetPathAndWait(tempPath);
            unit.DeselectRange();
            //gameManager.menuLocation = tileClicked.transform.position;
            //gameManager.inGameUnitMenu.transform.position = gameManager.mainCamera.WorldToScreenPoint(gameManager.menuLocation);
            //gameManager.inGameUnitMenu.gameObject.SetActive(true); // consider changing this to a function in unit menu manager with parameter for the location
            gameManager.mainGameMenuManager.OpenMenu(Define.MenuType.UnitMenu, new List<Define.MenuType>(), gameManager.selectedPlayer, tileClicked.transform.position);
            gameManager.SetState(new MenuState(gameManager));
        }

        yield break;
    }

    public override IEnumerator ClickEmptyTile(MapTileController tileClicked)
    {
        UnitController selectedPlayer = gameManager.selectedPlayer;
        Character selectedCharacter = selectedPlayer.Character;
        MapTileController selected = tileClicked; // Change this
        MapTileController tempPlayer = gameManager.levelMapManager.GetValue(selectedPlayer.Location);

        List<MapTileController> tempPath = gameManager.pathfinder.Pathfind(tempPlayer, selected, selectedCharacter.unitType, selectedCharacter.unitAllignment, selectedCharacter.Move.value);

        if (tempPath.Count <= selectedCharacter.Move.value && tempPath.Count !=0) 
        {
            gameManager.StartCoroutine(selectedPlayer.SetPathAndWait(tempPath));
            //gameManager.menuLocation = tileClicked.transform.position;
            //gameManager.inGameUnitMenu.transform.position = gameManager.mainCamera.WorldToScreenPoint(gameManager.menuLocation);
            //gameManager.inGameUnitMenu.gameObject.SetActive(true); // consider changing this to a function in unit menu manager with parameter for the location

            gameManager.mainGameMenuManager.OpenMenu(Define.MenuType.UnitMenu, new List<Define.MenuType>(), selectedPlayer, tileClicked.transform.position + gameManager.tileMapGrid.cellSize.x*Vector3.right*1.5f);
            gameManager.SetState(new MenuState(gameManager));
        }

        yield break;
    }

    public override IEnumerator Cancel()
    {
        gameManager.selectedPlayer.DeselectRange();
        gameManager.SetState(new PlayerTurnState(gameManager));
        yield break;
    }
}
