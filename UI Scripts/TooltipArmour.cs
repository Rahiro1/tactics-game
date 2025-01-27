using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipArmour : MonoBehaviour
{
    public RectTransform rectTransform;
    public WeaponRankDisplay primaryWeaponRank, secondaryWeaponRank, tertiaryWeaponRank;
    public TextMeshProUGUI nameText, armourValueText, defenceValueText, magicValueText, weaponComplexityValueText, resistanceValueText, descriptionText;
    public Camera cam;

    public void ShowTooltip(Armour armour, Vector3 position)
    {

        // display Armour type
        primaryWeaponRank.DisplayWeaponRank(armour.weaponType, armour.weaponRank, armour.weaponMasteryLevel);

        if (armour.secondaryWeaponType != Define.WeaponType.none)
        { 
            secondaryWeaponRank.DisplayWeaponRank(armour.secondaryWeaponType, armour.secondaryWeaponRank, armour.secondaryWeaponMasteryLevel);
        }
        else
        {
            secondaryWeaponRank.Hide();
        }

        if (armour.secondaryWeaponType != Define.WeaponType.none)
        {
            tertiaryWeaponRank.DisplayWeaponRank(armour.tertiaryWeaponType, armour.tertiaryWeaponRank, armour.tertiaryWeaponMasteryLevel);
        }
        else
        {
            tertiaryWeaponRank.Hide();
        }

        // set text 
        nameText.text = armour.ItemName;
        armourValueText.text = armour.armourValue.ToString();
        defenceValueText.text = armour.armourDefence.ToString();
        magicValueText.text = armour.bonusMagic.ToString();
        weaponComplexityValueText.text = armour.armourComplexity.ToString();
        resistanceValueText.text = armour.bonusResistance.ToString();
        descriptionText.text = armour.ItemDescription.ToString();


        float pivotX = cam.WorldToScreenPoint(position).x / Screen.width;
        float pivotY = cam.WorldToScreenPoint(position).y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;

        gameObject.SetActive(true);

    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
}
