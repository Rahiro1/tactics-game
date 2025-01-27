using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressMetrics
{
    public int chapterNumber;
    public int gold;
    public float time;
    // time spent playing
    // notable bosses?

    public ProgressMetrics(int chapterNumber, int playergold, float time)
    {
        this.chapterNumber = chapterNumber;
        gold = playergold;
        this.time = time;
    }
}
