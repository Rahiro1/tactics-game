using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour
{
    // this method of referencing assets will mean that saves become incommpatible with later versions if existing IDs are changed

    public static Database Instance;
    // this class is simply for storing assets that will be used by playerdata, such as creating character for the first time - has been extended to initialise dictionaries for the project
    public List<CharacterSO> characterList;
    public Dictionary<int,CharacterSO> CharacterDictionary { get; private set; }
    public List<ClassSO> classList;
    public Dictionary<int, ClassSO> classDictionary { get; private set; }
    public List<SkillSO> skillsList;
    public Dictionary<int,SkillSO> skillDictionary { get; private set; }
    public List<LevelSO> LevelsList;
    public Dictionary<int,LevelSO> LevelDictionary { get; private set; }
    public List<MapTileSO> tileMoveCostsList;
    public Dictionary<Define.TileType,MapTileSO> tileMoveDictionary { get; private set; }
    public List<ItemSO> itemList;
    public Dictionary<int, ItemSO> itemDictionary { get; private set; }
    public List<Define.GenericWeaponIconData> genericWeaponIcons;
    public Dictionary<Define.WeaponType, Sprite> genericWeaponIconDictionary { get; private set; }
    public List<Define.GenericArmourIconData> genericArmourIcons;
    public Dictionary<Define.ArmourType, Sprite> genericArmourIconDictionary { get; private set; }
    public Sprite weaponButtonBackground, armourButtonBackground, otherItemButtonBackground;
    

    private void Awake()
    {
        // singleton code
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;

        //assigning to dictionaries
        CharacterDictionary = new Dictionary<int, CharacterSO>();
        LevelDictionary = new Dictionary<int, LevelSO>();
        tileMoveDictionary = new Dictionary<Define.TileType, MapTileSO>();
        classDictionary = new Dictionary<int, ClassSO>();
        itemDictionary = new Dictionary<int, ItemSO>();
        skillDictionary = new Dictionary<int, SkillSO>();
        genericWeaponIconDictionary = new Dictionary<Define.WeaponType, Sprite>();
        genericArmourIconDictionary = new Dictionary<Define.ArmourType, Sprite>();


        foreach (CharacterSO character in characterList)
        {
            CharacterDictionary.Add(character.characterID, character);
        }
        foreach(LevelSO level in LevelsList)
        {
            LevelDictionary.Add(level.levelID, level);
        }
        foreach (MapTileSO moveCostList in tileMoveCostsList)
        {
            tileMoveDictionary.Add(moveCostList.TileType, moveCostList);
        }
        foreach(ClassSO classSO in classList)
        {
            classDictionary.Add(classSO.classID, classSO);
        }
        foreach(ItemSO item in itemList)
        {
            itemDictionary.Add(item.itemID, item);
        }
        foreach(SkillSO skill in skillsList)
        {
            skillDictionary.Add(skill.skillID, skill);
        }
        foreach (Define.GenericWeaponIconData iconData in genericWeaponIcons)
        {
            genericWeaponIconDictionary.Add(iconData.weaponType, iconData.genericWeaponSprite);
        }
        foreach (Define.GenericArmourIconData iconData in genericArmourIcons)
        {
            genericArmourIconDictionary.Add(iconData.armourType, iconData.genericArmourSprite);
        }
    }

    private void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}
