using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GenericController : UnitController
{
    //UI elements


    private void Awake()
    {

    }

    public void InitialiseGeneric(Define.GenericEnemyData unitData)
    {
        Character = new Character(unitData);
        Location = unitData.position;
        gameManager = GameManager.Instance;
        enemyRangeHighlighted = null;
        //Debug.Log("Assigning sprite");
        unitSprite.sprite = Character.GetCharacterSprite();
        Character.currentHP = Character.MaxHP.GetModifiedValue();
        Character.currentArmour = Character.MaxArmour;
        BattalionNumber = unitData.battalionNumber;
        aIType = unitData.AIType;
        activatedAIType = unitData.SecondaryAIType;
        isActivated = false;
        UpdateHealthBar();

        switch (unitData.allignment)
        {
            // TODO - cache these colours for performance
            case Define.UnitAllignment.Player:
                unitSprite.color = new Color32(220, 220, 255, 255);
                break;
            case Define.UnitAllignment.Enemy:
                unitSprite.color = new Color32(255, 220, 220, 255);
                break;
            case Define.UnitAllignment.Ally:
                unitSprite.color = new Color32(255, 255, 220, 255);
                break;
            case Define.UnitAllignment.Other:
                unitSprite.color = new Color32(220, 255, 220, 255);
                break;
        }
    }
    
}
