using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Character")]
public class CharacterSO : ScriptableObject
{
    public string characterName;
    public int characterID;
    public ClassSO baseClass;
    public Sprite characterSprite;
    public AnimatorOverrideController animatorOverrideController;
    public Define.UnitAllignment unitAllignment;
    public Define.AIType AIType;
    public Define.AIType SecondaryAIType;
    public int battalionNumber;
    public int level;
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
    public WeaponSO equippedStartingWeapon;
    public ArmourSO equippedStartingArmour;
    public List<ItemSO> startingInventory;
    public List<SkillSO> uniqueSkillList;
    // add skill
    // add portrait
    // add sprite

    // CONSIDER - changing this to list of intergers, and initialise the list, setting everything else to zero.

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
}
