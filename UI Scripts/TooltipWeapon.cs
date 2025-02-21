using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TooltipWeapon : MonoBehaviour
{
    public RectTransform rectTransform;
    public WeaponRankDisplay primaryWeaponRank, secondaryWeaponRank, tertiaryWeaponRank;
    public TextMeshProUGUI nameText, powerValueText, offenceValueText, defenceValueText, rendingValueText, weaponComplexityValueText, rangeValueText, criticalValueText, descriptionText;
    public Camera cam;

    public  void ShowTooltip(Weapon weapon, Vector3 position)
    {

        // display weapon ranks
        primaryWeaponRank.DisplayWeaponRank(weapon.weaponType, weapon.weaponRank, weapon.weaponMasteryLevel);

        if(weapon.secondaryWeaponType != Define.WeaponType.none)
        {
            secondaryWeaponRank.DisplayWeaponRank(weapon.secondaryWeaponType, weapon.secondaryWeaponRank, weapon.secondaryWeaponMasteryLevel);
        }
        else
        {
            secondaryWeaponRank.Hide();
        }

        if (weapon.tertiaryWeaponType != Define.WeaponType.none)
        {
            tertiaryWeaponRank.DisplayWeaponRank(weapon.tertiaryWeaponType, weapon.tertiaryWeaponRank, weapon.tertiaryWeaponMasteryLevel);
        }
        else
        {
            tertiaryWeaponRank.Hide();
        }

        // set text 
        nameText.text = weapon.ItemName;
        powerValueText.text = weapon.power.ToString();
        offenceValueText.text = weapon.offence.ToString();
        defenceValueText.text = weapon.defence.ToString();
        rendingValueText.text = weapon.rending.ToString();
        weaponComplexityValueText.text = weapon.complexity.ToString();
        rangeValueText.text = weapon.range.ToString();
        criticalValueText.text = weapon.criticalRate.ToString();
        descriptionText.text = weapon.ItemDescription.ToString();


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
