using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject PlayerUnitPrefab;

    public Transform[] playerSpawnPos;

    public int playerNum;

    public NetworkManager networkManager;

    public bool spawned = false;

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

        if (SceneManager.GetActiveScene().name == "Hub" && !spawned)
        {

            CmdSpawnPlayer();
            spawned = true;

        }


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
            playerNum = 1;
            Debug.Log("Server");
        }
        else
        {
            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[1].position, playerSpawnPos[1].rotation);
            playerNum = 2;
            Debug.Log("Client");
        }



        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

    }

}
