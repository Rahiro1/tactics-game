using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WexpDisplay : MonoBehaviour
{
    private float wexpUpdateTotalTime = 0.5f;
    private WaitForSeconds wexpSliderWait;
    private WaitForSeconds waitTimeAdditional = new WaitForSeconds(0.2f);
    public Image weaponIcon;
    public Slider wexpSlider;

    public IEnumerator LoadMenu(LevelCounter weaponRank, int increase)
    {
        float secondsForWexpUpdate = wexpUpdateTotalTime / increase;
        wexpSliderWait = new WaitForSeconds(secondsForWexpUpdate);
        weaponIcon.sprite = Database.Instance.genericWeaponIconDictionary[weaponRank.weaponType];

        int wexpValue = weaponRank.Experience - increase;
        if(wexpValue < 0)
        {
            wexpValue += 100;
        }
        wexpSlider.value = wexpValue;

        gameObject.SetActive(true);


        yield return waitTimeAdditional;
        //move this to new class?

        for(int i = 0; i<increase; i++)
        {
            wexpValue += 1;
            if(wexpValue > 99)
            {
                wexpValue = 0;
            }

            wexpSlider.value = wexpValue;

            yield return wexpSliderWait;
        }

        yield return waitTimeAdditional;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
}
