using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpDisplay : MonoBehaviour
{
    private float expUpdateTotalTime = 0.5f;
    public Slider expSlider;
    public WexpDisplay pWexpDisplay, sWexpDisplay, tWexpDisplay;
    private WaitForSeconds expSliderWait;
    private WaitForSeconds waitTimeAdditional = new WaitForSeconds(0.2f);


    public IEnumerator OpenMenu(LevelCounter level , int expIncrease, LevelCounter pWeaponRank, int pWexpIncrease, LevelCounter sWeaponRank, int sWexpIncrease, LevelCounter tWeaponRank, int tWexpIncrease)
    {
        expSlider.value = level.Experience;
        float secondsForWexpUpdate = expUpdateTotalTime / expIncrease;
        int expValue = level.Experience;
        if (expValue < 0)
        {
            expValue += 100;
        }
        expSlider.value = expValue;
        gameObject.SetActive(true);

        if (pWeaponRank != null)
        {
            StartCoroutine(pWexpDisplay.LoadMenu(pWeaponRank, pWexpIncrease));
        }
        else
        {
            pWexpDisplay.CloseMenu();
        }
        if (sWeaponRank != null)
        {
            StartCoroutine(sWexpDisplay.LoadMenu(sWeaponRank, sWexpIncrease));
        }
        else
        {
            sWexpDisplay.CloseMenu();
        }
        if (tWeaponRank != null)
        {
            StartCoroutine(tWexpDisplay.LoadMenu(tWeaponRank, tWexpIncrease));
        }
        else
        {
            tWexpDisplay.CloseMenu();
        }

        yield return waitTimeAdditional;

        for (int i = 0; i < expIncrease; i++)
        {
            expValue += 1;
            if (expValue > 99)
            {
                expValue = 0;
            }

            expSlider.value = expValue;

            yield return expSliderWait;
        }


        yield return waitTimeAdditional;

        CloseMenu();

        yield break;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
     

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
