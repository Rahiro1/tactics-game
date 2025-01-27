using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Assets/Class")]
public class ClassSO : ScriptableObject
{
    public int classID;
    public string className;
    public Sprite genericClassSprite; // for generic enemies
    public int baseHP;
    public int baseStrength;
    public int baseMagic;
    public int baseOffence;
    public int baseDefence;
    public int baseResistance;
    public int baseSpeed;
    public int baseMove;
    public int growthHP;
    public int growthStrength;
    public int growthMagic;
    public int growthOffence;
    public int growthDefence;
    public int growthResistance;
    public int growthSpeed;
    public int maxHP;
    public int maxStrength;
    public int maxMagic;
    public int maxOffence;
    public int maxDefence;
    public int maxResistance;
    public int maxSpeed;
    public int maxMove;
    // add class skill list 
    // add generic portrait
    public int enemyGrowthHP;
    public int enemyGrowthStrength;
    public int enemyGrowthMagic;
    public int enemyGrowthOffence;
    public int enemyGrowthDefence;
    public int enemyGrowthResistance;
    public int enemyGrowthSpeed;

    public int swordRank;
    public int swordMastery;
    public int spearRank;
    public int spearMastery;
    public int axeRank;
    public int axeMastery;
    public int bowRank;
    public int bowMastery;
    public int elementalRank;
    public int elementalMastery;
    public int decayRank;
    public int decayMastery;
    public int healRank;
    public int healMastery;
    public int armourWeaponRank;
    public int armourWeaponMastery;
    public int creationRank;
    public int creationMastery;

    public Define.UnitType unitType;
    public List<Define.ArmourType> allowedArmourTypes;

    public List<SkillSO> classSkillList;
}
