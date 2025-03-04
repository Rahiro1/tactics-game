using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersController : UnitController
{
    // TODO Characters controller
    public CharacterSO characterToLoad;  // the character that will be loaded by loadlevelstate, asigned in editor
    
    public void InitialiseCharacter(Character character, Vector3Int location) // initialise with a character as opposed to creating a new one for a generic unit
    {
        // TODO recheck charactersController InitialiseCharacter method
        this.Character = character;
        Location = location;
        startOfTurnLocation = location;
        gameManager = GameManager.Instance; // CONSIDER some of this code is repeated in genericController -> could make a virtual method in unitcontroller called InitialiseUnit() and override here 
        enemyRangeHighlighted = null;
        currentHP = character.HP.GetModifiedValue();
        currentArmour = maxArmour;
        unitSprite.sprite = character.GetCharacterSprite();
        BattalionNumber = character.battalionNumber;
        aIType = character.AIType;
        activatedAIType = character.secondaryAIType;
        isActivated = false;
        UpdateHealthBar();
        
    }
    
}
