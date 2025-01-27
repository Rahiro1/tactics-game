using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponRankDisplay : MonoBehaviour
{
    public Image weaponIcon, masteryRankTwo, masteryRankThree;      // mastery rank one is implied by the presence of the icon
    public TextMeshProUGUI weaponRankText;

    public void DisplayWeaponRank(Define.WeaponType weaponType, int weaponRank, int weaponMastery)
    {
        weaponIcon.sprite = Database.Instance.genericWeaponIconDictionary[weaponType];
        weaponRankText.text = weaponRank.ToString();

        //if(weaponMastery == 0)
        //{
        //   weaponRankText.text = "0";
        //}
        if(weaponMastery > 1)
        {
            masteryRankTwo.gameObject.SetActive(true);
        }
        else
        {
            masteryRankTwo.gameObject.SetActive(false);
        }
        if (weaponMastery > 2)
        {
            masteryRankThree.gameObject.SetActive(true);
        }
        else
        {
            masteryRankThree.gameObject.SetActive(false);
        }

        if(weaponMastery == 0)
        {
            Hide();
        }
        else
        {
            gameObject.SetActive(true);
        }
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
