using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NameData
{

    public string name;

    public NameData(MenuController mc)
    {

        name = mc.playerName;

    }

    public NameData()
    {

        name = "Player";

    }

}
