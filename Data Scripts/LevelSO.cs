using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Assets/Level")]
public class LevelSO : ScriptableObject
{
    public string levelName = "New Level";
    public int levelID;
    public bool hasPrecombatPhase;
    public bool isFinalLevel;
    public int mapWidth;
    public int mapHeight;
    public Tilemap tileMap;
    public Tilemap aestheticTilemap;
    public StorySO startOfLevelStory;
    public StorySO endOfLevelStory;
    //public int[] enemyLevels;
    //public ClassSO[] enemyList;
    //public Vector3Int[] enemyLocations;
    public List<WinConditionSO> victoryConditionsList;
    public List<WinConditionSO> defeatConditionsList;
    public List<BonusConditionSO> bonusConditionsList;
    public List<Define.BattalionData> battalions;
    public List<Define.GenericEnemyData> GenericEnemyList;  // for generic units that start on the map
    public List<Define.CharacterData> levelCharacterList;   // for characters that start on the map (excluding players)
    public List<ReinforcementSO> reinforcmentList;
    public List<Vector3Int> playerPositions;
    public List<int> forcedDeployments;
    public List<Define.MapEventData> mapEventList;
    public ShopSO armoury;
    public ShopSO shop;
    public Vector3Int thiefEscapeLocation;
    public GameObject background;




    

}
