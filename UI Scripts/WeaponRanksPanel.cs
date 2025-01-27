using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRanksPanel : MonoBehaviour
{
    [SerializeField] List<WeaponRankDisplay> weaponRankDisplays;


    public void LoadMenu(Character character)
    {
        foreach (WeaponRankDisplay weaponRank in weaponRankDisplays)
        {
            weaponRank.Hide();
        }

        int count = 0;
        foreach (LevelCounter weaponRank in character.weaponRanks)
        {
            if (count >= weaponRankDisplays.Count)
            {
                continue;
            }

            weaponRankDisplays[count].DisplayWeaponRank(weaponRank.weaponType, weaponRank.Level, weaponRank.MasteryLevel);
            count++;
        }
    }
}
