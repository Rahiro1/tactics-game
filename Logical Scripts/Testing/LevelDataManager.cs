using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;



public class LevelDataManager : MonoBehaviour   // make name more intuitive
{
    // inspired by tarodev on youtube

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int levelIndex;
    private List<Define.GenericEnemyData> genericEnemeyDatas = new List<Define.GenericEnemyData>();
    private GenricEditorUnit[] genericsArray;
    private List<Define.CharacterData> levelCharacterDatas = new List<Define.CharacterData>();
    private CharacterEditorUnit[] levelCharactersArray;
    [SerializeField] private LevelSO levelToLoad;
    [SerializeField] private GameObject genericUnitPrefab;
    [SerializeField] private GameObject characterUnitPrefab;
    [SerializeField] private Grid gridToLoadTo;

    public void SaveLevel()
    {
        // clear the list as the list will NOT empty between saves 
        genericEnemeyDatas.Clear();
        
        genericsArray = FindObjectsByType<GenricEditorUnit>(FindObjectsSortMode.None); // consider changing to by tag

        for (int i =0; i< genericsArray.Length; i++)
        {
            Define.GenericEnemyData unitData = genericsArray[i].genericEnemyData;
            unitData.position = gridToLoadTo.WorldToCell(genericsArray[i].transform.position);
            genericEnemeyDatas.Add(unitData);
        }

        levelCharacterDatas.Clear();
        levelCharactersArray = FindObjectsByType<CharacterEditorUnit>(FindObjectsSortMode.None); // consider changing to by tag

        for (int i = 0; i < levelCharactersArray.Length; i++)
        {
            Define.CharacterData unitData = new Define.CharacterData();
            unitData.character = levelCharactersArray[i].characterSO;
            unitData.position = gridToLoadTo.WorldToCell(levelCharactersArray[i].transform.position);
            levelCharacterDatas.Add(unitData);
        }

        // create scriptable object and assign data to it

        LevelSO newLevel = ScriptableObject.CreateInstance<LevelSO>();
        newLevel.name = "Level" + levelIndex;
        newLevel.levelID = levelIndex;
        // this gives a typematch error and I don't know why newLevel.tileMap = tilemap;
        newLevel.GenericEnemyList = genericEnemeyDatas;
        newLevel.levelCharacterList = levelCharacterDatas;

        ScriptableObjectUtility.SaveLevelSO(newLevel);
    }

    public void ClearLevel()
    {
        Tilemap[] tilemaps = FindObjectsOfType<Tilemap>();
        GenericController[] generics = FindObjectsOfType<GenericController>();


        foreach (Tilemap map in tilemaps)
        {
            map.ClearAllTiles();
        }
    }

    public void LoadLevel()
    {
        ClearLevel();

        Instantiate(levelToLoad.tileMap, gridToLoadTo.transform);
        foreach ( Define.GenericEnemyData unitData in levelToLoad.GenericEnemyList)
        {
            GenricEditorUnit unit = GameObject.Instantiate(genericUnitPrefab, gridToLoadTo.CellToWorld(unitData.position), Quaternion.identity).GetComponent<GenricEditorUnit>();
            unit.genericEnemyData = unitData;
        }
        foreach (Define.CharacterData unitData in levelToLoad.levelCharacterList)
        {
            CharacterEditorUnit unit = GameObject.Instantiate(characterUnitPrefab, gridToLoadTo.CellToWorld(unitData.position), Quaternion.identity).GetComponent<CharacterEditorUnit>();
            unit.characterSO = unitData.character;
        }
    }

}


#if UNITY_EDITOR
public static class ScriptableObjectUtility
{
    public static void SaveLevelSO(LevelSO level)
    {
        AssetDatabase.CreateAsset(level, $"Assets/Resources/Levels/{level.name}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}


#endif

