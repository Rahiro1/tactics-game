using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/ReinforcementGroup/Zone")]
public class ReinforcementZoneSO : ReinforcementSO
{
    public int zoneMaxX;
    public int zoneMinX;
    public int zoneMaxY;
    public int zoneMinY;
    public bool hasTriggered = false;

    public override void StandardActivation()
    {
        if (!hasTriggered)
        {
            bool shouldTrigger = false;
            foreach(UnitController unit in GameManager.Instance.playerList)
            {
                if (unit.Location.x <= zoneMaxX && unit.Location.x >= zoneMinX)     // is unit within box?
                {
                    if(unit.Location.y <= zoneMaxY && unit.Location.y >= zoneMinY)
                    {
                        shouldTrigger = true;
                    }
                }
            }

            if (shouldTrigger)
            {
                hasTriggered = true;
                AddReinforcements();
            }
            
        }
    }
}
