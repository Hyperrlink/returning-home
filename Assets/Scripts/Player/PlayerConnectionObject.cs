﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject PlayerUnitPrefab;

    public Transform[] playerSpawnPos;

    public int playerNum = 0;
    public string playerName;

    public NetworkManager networkManager;

    public bool startedServer = false;
    public bool hubSpawned = false;
    public bool forestSpawned = false;
    public bool waterSpawned = false;
    public bool rockSpawned = false;

    // Completed levels
    public bool completedForestLevel;
    public bool completedWaterLevel;
    public bool completedCastleLevel;
    public bool completedRockLevel;

    public bool saved = false;

    void Start()
    {

        DontDestroyOnLoad(gameObject);

        if (!isLocalPlayer)
        {
            return;
        }

    }

    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        // Save data
        if (SceneManager.GetActiveScene().name == "Hub" && !saved && playerNum == 1)
        {
            SaveSystem.SaveLevelData(this);
            saved = true;
        }

        if (SceneManager.GetActiveScene().name == "Hub" && !hubSpawned)
        {

            CmdSpawnPlayer();
            hubSpawned = true;
            saved = false;
            forestSpawned = false;
            waterSpawned = false;
            rockSpawned = false;

        }

        if (SceneManager.GetActiveScene().name == "Forest Level" && !forestSpawned)
        {

            CmdSpawnPlayer();
            forestSpawned = true;
            hubSpawned = false;
            waterSpawned = false;
            rockSpawned = false;

        }

        if (SceneManager.GetActiveScene().name == "Water Level" && !waterSpawned)
        {

            CmdSpawnPlayer();
            waterSpawned = true;
            forestSpawned = false;
            hubSpawned = false;
            rockSpawned = false;

        }

        if (SceneManager.GetActiveScene().name == "Rock Level" && !rockSpawned)
        {

            CmdSpawnPlayer();
            rockSpawned = true;
            waterSpawned = false;
            forestSpawned = false;
            hubSpawned = false;

        }

    }

    // Commands

    [Command]
    void CmdSpawnPlayer()
    {
        if (connectionToClient.isReady)
        {
            Spawn();
        } else
        {
            StartCoroutine(WaitForReady());
        }

    }

    IEnumerator WaitForReady()
    {

        while (!connectionToClient.isReady)
        {
            yield return new WaitForSeconds(0.25f);
        }
        Spawn();

    }

    [Server]
    void Spawn()
    {

        playerSpawnPos = new Transform[2];

        playerSpawnPos[0] = GameObject.FindGameObjectsWithTag("PlayerSpawn")[0].GetComponent<Transform>();
        playerSpawnPos[1] = GameObject.FindGameObjectsWithTag("PlayerSpawn")[1].GetComponent<Transform>();

        GameObject go;

        if ((NetworkServer.connections.Count == 1 && !startedServer) || playerNum == 1)
        {
            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[0].position, playerSpawnPos[0].rotation);
            playerNum = 1;
            go.GetComponent<PlayerMovement>().playerNum = 1;
            //Debug.Log("Server");

            // Load save
            LevelData data = SaveSystem.LoadLevelData();
            completedForestLevel = data.completedForestLevel;
            completedWaterLevel = data.completedWaterLevel;
            completedCastleLevel = data.completedCastleLevel;
            completedRockLevel = data.completedRockLevel;

            NameData nameData = SaveSystem.LoadNameData();
            playerName = nameData.name;

            go.GetComponent<PlayerMovement>().playerName = playerName;
        }
        else if ((NetworkServer.connections.Count == 2 && !startedServer) || playerNum == 2)
        {
            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[1].position, playerSpawnPos[1].rotation);
            playerNum = 2;
            go.GetComponent<PlayerMovement>().playerNum = 2;
            //Debug.Log("Client");

            // Load save
            // Get host player's gameobject

            PlayerConnectionObject[] pcos;
            PlayerConnectionObject hostPlayer;

            pcos = new PlayerConnectionObject[2];

            GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerConnectionObject");

            pcos[0] = temp[0].GetComponent<PlayerConnectionObject>();
            pcos[1] = temp[1].GetComponent<PlayerConnectionObject>();

            if (pcos[0].playerNum == 1)
                hostPlayer = pcos[0];
            else
                hostPlayer = pcos[1];

            completedForestLevel = hostPlayer.completedForestLevel;
            completedWaterLevel = hostPlayer.completedWaterLevel;
            completedCastleLevel = hostPlayer.completedCastleLevel;
            completedRockLevel = hostPlayer.completedRockLevel;

            NameData nameData = SaveSystem.LoadNameData();
            playerName = nameData.name;

            go.GetComponent<PlayerMovement>().playerName = playerName;
        }
        else
        {

            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[0].position, playerSpawnPos[0].rotation);
            playerNum = 1;
            go.GetComponent<PlayerMovement>().playerNum = 1;
            //Debug.Log("Server");

            // Load save
            LevelData data = SaveSystem.LoadLevelData();
            completedForestLevel = data.completedForestLevel;
            completedWaterLevel = data.completedWaterLevel;
            completedCastleLevel = data.completedCastleLevel;
            completedRockLevel = data.completedRockLevel;

            NameData nameData = SaveSystem.LoadNameData();
            playerName = nameData.name;

            go.GetComponent<PlayerMovement>().playerName = playerName;

        }

        startedServer = true;

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

    }

}
