using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject PlayerUnitPrefab;

    public Transform[] playerSpawnPos;


    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        CmdSpawnPlayer();

    }

    void Update()
    {
        
    }

    // Commands

    [Command]
    void CmdSpawnPlayer()
    {

        playerSpawnPos = new Transform[2];

        playerSpawnPos[0] = GameObject.FindGameObjectsWithTag("PlayerSpawn")[0].GetComponent<Transform>();
        playerSpawnPos[1] = GameObject.FindGameObjectsWithTag("PlayerSpawn")[1].GetComponent<Transform>();

        GameObject go;

        if (NetworkServer.connections.Count == 1)
        {
            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[0].position, playerSpawnPos[0].rotation);
            Debug.Log("Server");
        } else
        {
            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[1].position, playerSpawnPos[1].rotation);
        }

<<<<<<< HEAD
        go.GetComponent<PlayerMovement>().playerNum = playerNum;
        go.GetComponent<PlayerMovement>().parent = gameObject;

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

}

}
