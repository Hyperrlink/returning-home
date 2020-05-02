using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelManager : NetworkBehaviour
{

    public Transform[] forestPadTriggers;
    public Transform[] waterPadTriggers;
    public Transform[] rockPadTriggers;

    PlayerConnectionObject[] pcos;
    PlayerConnectionObject hostPlayer;
    bool gotHost = false;

    public Material[] materials;

    public bool player1ReadyF = false;
    public bool player2ReadyF = false;
    public bool player1ReadyW = false;
    public bool player2ReadyW = false;
    public bool player1ReadyR = false;
    public bool player2ReadyR = false;

    public float radius;
    public float maxDistance;

    public LayerMask playerLayer;

    void Start()
    {

        

    }

    void FixedUpdate()
    {

        if (!hasAuthority)
        {
            return;
        }

        // Get host player's gameobject
        if (!gotHost)
        {
            pcos = new PlayerConnectionObject[2];

            GameObject[] temp = GameObject.FindGameObjectsWithTag("PlayerConnectionObject");

            if (NetworkServer.connections.Count == 2)
            {
                pcos[0] = temp[0].GetComponent<PlayerConnectionObject>();
                pcos[1] = temp[1].GetComponent<PlayerConnectionObject>();

                if (pcos[0].playerNum == 1)
                    hostPlayer = pcos[0];
                else
                    hostPlayer = pcos[1];
            } else
            {
                hostPlayer = temp[0].GetComponent<PlayerConnectionObject>();
            }
            
            gotHost = true;
        }

        // Check for level change
        RaycastHit hit;

        foreach (Transform forestPadTrigger in forestPadTriggers)
        {

            player1ReadyF = Physics.SphereCast(forestPadTrigger.position, radius, forestPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
            player2ReadyF = Physics.SphereCast(forestPadTrigger.position, radius, forestPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;

        }

        foreach (Transform waterPadTrigger in waterPadTriggers)
        {

            player1ReadyW = Physics.SphereCast(waterPadTrigger.position, radius, waterPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
            player2ReadyW = Physics.SphereCast(waterPadTrigger.position, radius, waterPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;

        }

        foreach (Transform rockPadTrigger in rockPadTriggers)
        {

            player1ReadyR = Physics.SphereCast(rockPadTrigger.position, radius, rockPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
            player2ReadyR = Physics.SphereCast(rockPadTrigger.position, radius, rockPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;

        }


        if (player1ReadyF || player2ReadyF)
        {

            Debug.Log("Everyone is ready!");

            NetworkManager.singleton.ServerChangeScene("Forest Level");
            SceneManager.LoadScene("Forest Level");

        } else if (player1ReadyW || player2ReadyW)
        {

            Debug.Log("Everyone is ready!");

            NetworkManager.singleton.ServerChangeScene("Water Level");
            SceneManager.LoadScene("Water Level");

        } else if (player1ReadyR || player2ReadyR)
        {

            Debug.Log("Everyone is ready!");

            NetworkManager.singleton.ServerChangeScene("Rock Level");
            SceneManager.LoadScene("Rock Level");

        }

        // Check for level complete
        if (hostPlayer.completedForestLevel)
        {
            forestPadTriggers[0].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[1];
            forestPadTriggers[1].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[1];
        } else
        {
            forestPadTriggers[0].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[0];
            forestPadTriggers[1].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[0];
        }

        if (hostPlayer.completedWaterLevel)
        {
            waterPadTriggers[0].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[1];
            waterPadTriggers[1].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[1];
        }
        else
        {
            waterPadTriggers[0].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[0];
            waterPadTriggers[1].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[0];
        }

        if (hostPlayer.completedRockLevel)
        {
            rockPadTriggers[0].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[1];
            rockPadTriggers[1].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[1];
        }
        else
        {
            rockPadTriggers[0].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[0];
            rockPadTriggers[1].transform.Find("OuterPart").GetComponent<Renderer>().sharedMaterial = materials[0];
        }

    }

}
