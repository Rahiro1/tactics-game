using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class FileSelectButton : MonoBehaviour
{
    // TODO - redo code to assign fileNumber automatically
    public int fileNumber;
    public TextMeshProUGUI slotNumberText, chapterNumberText, goldText, timeText;
    public void LoadButton()
    {
        JSONDataService jSONDataService = new JSONDataService();
        string relativePath = "/File" + fileNumber.ToString() + "Progress.json";        // CONSIDER - maybe save/load path should be stored in a util/define function (in number -> out path)
        if (File.Exists(Application.persistentDataPath + relativePath)){
            ProgressMetrics progressMetrics = jSONDataService.LoadData<ProgressMetrics>(relativePath);
            chapterNumberText.text = "Chapter - " + progressMetrics.chapterNumber.ToString();
            goldText.text = progressMetrics.gold.ToString() + "g";
            TimeSpan timeSpan = TimeSpan.FromSeconds(progressMetrics.time);
            timeText.text = timeSpan.ToString(@"hh\:mm\:ss");
            slotNumberText.gameObject.SetActive(false);
            chapterNumberText.gameObject.SetActive(true);
            goldText.gameObject.SetActive(true);
            timeText.gameObject.SetActive(true);
        }
        else
        {
            slotNumberText.text = "New File";
            slotNumberText.gameObject.SetActive(true);
            chapterNumberText.gameObject.SetActive(false);
            goldText.gameObject.SetActive(false);
            timeText.gameObject.SetActive(false);
        }
        
    }
}
