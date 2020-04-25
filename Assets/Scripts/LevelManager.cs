using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelManager : NetworkBehaviour
{

    public Transform[] forestPadTriggers;
    public Transform[] waterPadTriggers;

    PlayerConnectionObject[] pcos;
    PlayerConnectionObject hostPlayer;

    public bool player1Ready = false;
    public bool player2Ready = false;

    public float radius;
    public float maxDistance;

    public LayerMask playerLayer;

    public string levelName = "";

    void Start()
    {

        // Get host player's gameobject
        pcos = new PlayerConnectionObject[2];

        GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerConnectionObject");

        pcos[0] = temp[0].GetComponent<PlayerConnectionObject>();
        pcos[1] = temp[1].GetComponent<PlayerConnectionObject>();

        if (pcos[0].playerNum == 1)
            hostPlayer = pcos[0];
        else
            hostPlayer = pcos[1];

        GameObject[] temp1 = GameObject.FindGameObjectsWithTag("ForestLevelPadTrigger");

        forestPadTriggers = new Transform[2];

        if (temp1.Length == 0)
        {
            Debug.Log("Shit");
        } else
        {
            forestPadTriggers[0] = temp1[0].transform;
            forestPadTriggers[1] = temp1[1].transform;
        }

        GameObject[] temp2 = GameObject.FindGameObjectsWithTag("WaterLevelPadTrigger");

        waterPadTriggers = new Transform[2];

        if (temp2.Length == 0)
        {
            Debug.Log("Shit");
        }
        else
        {
            waterPadTriggers[0] = temp2[0].transform;
            waterPadTriggers[1] = temp2[1].transform;
        }

    }

    void FixedUpdate()
    {

        if (!hasAuthority)
        {
            return;
        }

        // Check for level change
        RaycastHit hit;

        foreach (Transform forestPadTrigger in forestPadTriggers)
        {

            player1Ready = Physics.SphereCast(forestPadTrigger.position, radius, forestPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
            player2Ready = Physics.SphereCast(forestPadTrigger.position, radius, forestPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;
            levelName = "Forest Level";

        }

        foreach (Transform waterPadTrigger in waterPadTriggers)
        {

            player1Ready = Physics.SphereCast(waterPadTrigger.position, radius, waterPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
            player2Ready = Physics.SphereCast(waterPadTrigger.position, radius, waterPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;
            levelName = "Water Level";

        }


        if (player1Ready || player2Ready)
        {

            Debug.Log("Everyone is ready!");

            NetworkManager.singleton.ServerChangeScene(levelName);
            SceneManager.LoadScene(levelName);

        }

        // Check for level complete
        if (hostPlayer.completedForestLevel)
        {
            
        }

    }

}
