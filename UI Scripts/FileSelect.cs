using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileSelect : MonoBehaviour
{
    private PlayerData playerData;
    private Define.MenuType menuType;
    public GameObject difficultySelect;
    public TitleScreenManager mainMenu;
    public List<FileSelectButton> fileButtons;
    

    public void OpenMenu(EventEnums eventEnum)
    {
        // changes what the buttons do - New game/load/save
        menuType = eventEnum.menuType;
        LoadButtons();
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void CloseToPreviousMenu()
    {
        switch (menuType)
        {
            case Define.MenuType.NewGameMenu:
                difficultySelect.SetActive(true);
                break;
            case Define.MenuType.LoadGameMenu:
                mainMenu.gameObject.SetActive(true);
                break;
            case Define.MenuType.SaveGameMenu:
                break;
            default:
                break;
        }
        gameObject.SetActive(false);
    }

    public void OnFileClicked(int fileNumber)
    {
        switch (menuType)
        {
            case Define.MenuType.NewGameMenu:
                OnNewGameClicked(fileNumber);
                break;
            case Define.MenuType.LoadGameMenu:
                OnLoadClicked(fileNumber);
                break;
            case Define.MenuType.SaveGameMenu:
                OnSaveClicked(fileNumber);
                break;
            default:
                break;
        }
    }

    public void LoadButtons()
    {
        foreach(FileSelectButton button in fileButtons)
        {
            button.LoadButton();
        }
    }



    private void OnNewGameClicked(int fileNumber)
    {
        playerData = new PlayerData();
        GameManager.Instance.playerData = playerData;
        playerData.NewGame(fileNumber);
        CloseMenu();
    }

    private void OnLoadClicked(int fileNumber)
    {
        JSONDataService jSONDataService = new JSONDataService();
        playerData = jSONDataService.LoadData<PlayerData>("/File" + fileNumber.ToString() + ".json");
        playerData.timeOfLastSave = Time.realtimeSinceStartup;
        GameManager.Instance.playerData = playerData;
        GameManager.Instance.StartLevel();
        CloseMenu();
    }

    private void OnSaveClicked(int fileNumber)
    {
        // TODO- move this to playerdatad class
        playerData = GameManager.Instance.playerData;
        //playerData.SaveGame(fileNumber);
        JSONDataService jSONDataService = new JSONDataService();
        jSONDataService.SaveData<PlayerData>("/File" + fileNumber.ToString() + ".json", playerData);
        ProgressMetrics progress = new ProgressMetrics(playerData.currentChapter, playerData.PlayerGold, playerData.AdjustTime());
        jSONDataService.SaveData<ProgressMetrics>("/File" + fileNumber.ToString() + "Progress.json", progress);
        CloseToPreviousMenu();
    }
}
