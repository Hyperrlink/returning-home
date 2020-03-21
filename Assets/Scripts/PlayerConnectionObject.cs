using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerConnectionObject : NetworkBehaviour
{

    public GameObject PlayerUnitPrefab;

    public Transform[] playerSpawnPos;

    public int playerNum = 0;

    public NetworkManager networkManager;

    public bool hubSpawned = false;
    public bool forestSpawned = false;
    public bool startedServer = false;

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

        if (SceneManager.GetActiveScene().name == "Hub" && !hubSpawned)
        {

            CmdSpawnPlayer();
            hubSpawned = true;
            forestSpawned = false;

        }

        if (SceneManager.GetActiveScene().name == "Forest Level" && !forestSpawned)
        {

            CmdSpawnPlayer();
            forestSpawned = true;
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
            Debug.Log("Server");
        }
        else if ((NetworkServer.connections.Count == 2 && !startedServer) || playerNum == 2)
        {
            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[1].position, playerSpawnPos[1].rotation);
            playerNum = 2;
            go.GetComponent<PlayerMovement>().playerNum = 2;
            Debug.Log("Client");
        }
        else
        {

            go = Instantiate(PlayerUnitPrefab, playerSpawnPos[0].position, playerSpawnPos[0].rotation);
            playerNum = 1;
            go.GetComponent<PlayerMovement>().playerNum = 1;
            Debug.Log("Server");

        }

        startedServer = true;

        NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

    }

}
