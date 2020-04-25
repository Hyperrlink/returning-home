using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{

    PlayerConnectionObject[] pcos;
    PlayerConnectionObject hostPlayer;

    

    void Start()
    {

        DontDestroyOnLoad(this);

        pcos = new PlayerConnectionObject[2];

        GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerConnectionObject");

        pcos[0] = temp[0].GetComponent<PlayerConnectionObject>();
        pcos[1] = temp[1].GetComponent<PlayerConnectionObject>();

        if (pcos[0].playerNum == 1)
            hostPlayer = pcos[0];
        else
            hostPlayer = pcos[1];

        LevelData data = SaveSystem.LoadLevelData();
        hostPlayer.completedForestLevel = data.completedForestLevel;
        hostPlayer.completedWaterLevel = data.completedWaterLevel;
        hostPlayer.completedCastleLevel = data.completedCastleLevel;
        hostPlayer.completedRockLevel = data.completedRockLevel;

    }

    void Update()
    {

   

        

    }

}
