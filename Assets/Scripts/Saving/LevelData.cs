using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{

    public bool completedForestLevel;
    public bool completedWaterLevel;
    public bool completedCastleLevel;
    public bool completedRockLevel;

    public LevelData (PlayerConnectionObject pco)
    {

        completedForestLevel = pco.completedForestLevel;
        completedWaterLevel = pco.completedWaterLevel;
        completedCastleLevel = pco.completedCastleLevel;
        completedRockLevel = pco.completedRockLevel;

    }

}
