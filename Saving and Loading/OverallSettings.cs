using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverallSettings
{
    public float masterVolume;

    

    public void SetToDefaults()
    {
        masterVolume = Define.MASTERVOLUMEDEFAULT;
    }
}
