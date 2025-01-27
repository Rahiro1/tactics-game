using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        //playerData = new PlayerData();
        //GameManager.Instance.playerData = playerData;
        //playerData.NewGame(1);
        //Debug.Log(Application.persistentDataPath);
    }

    public void OnNextLevelClick()
    {
        GameManager.Instance.EndLevel();
    }
}
