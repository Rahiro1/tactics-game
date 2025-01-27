using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider masterVolumeSlider;


    public void OpenMenu()
    {
        OverallSettings settings = GameManager.Instance.settings;

        masterVolumeSlider.value = settings.masterVolume;
        gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void SaveOptions()
    {
        OverallSettings settings = GameManager.Instance.settings;
        settings.masterVolume = masterVolumeSlider.value;
        JSONDataService jSONDataService = new JSONDataService();
        jSONDataService.SaveData<OverallSettings>(Define.SETTINGSRELATIVEPATH, settings);
    }
}
