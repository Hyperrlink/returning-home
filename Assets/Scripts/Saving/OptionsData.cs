using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionsData
{

    public float volume;
    public int qualityIndex;

    public OptionsData(MenuController mc)
    {

        volume = mc.musicVolume;
        qualityIndex = mc.qualityIndex;

    }

    public OptionsData()
    {

        volume = 1;
        qualityIndex = 2;

    }

}
