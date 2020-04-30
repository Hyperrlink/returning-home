using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EndLevel : NetworkBehaviour
{

    public bool triggered = false;

    public Transform[] endLevelPadTriggers;

    PlayerConnectionObject[] pcos;
    PlayerConnectionObject hostPlayer;
    bool gotHost = false;

    public bool player1Ready = false;
    public bool player2Ready = false;

    public float radius;
    public float maxDistance;

    public LayerMask playerLayer;

    void Start()
    {

        // Get end level pad triggers
        GameObject[] temp1 = GameObject.FindGameObjectsWithTag("EndLevelPadTrigger");

        endLevelPadTriggers = new Transform[2];

        if (temp1.Length == 0)
        {
            Debug.Log("Shit");
        }
        else
        {
            endLevelPadTriggers[0] = temp1[0].transform;
            endLevelPadTriggers[1] = temp1[1].transform;
        }

    }

    void Update()
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
            }
            else
            {
                hostPlayer = temp[0].GetComponent<PlayerConnectionObject>();
            }

            gotHost = true;
        }

        if (triggered)
        {

            endLevelPadTriggers[0].gameObject.SetActive(true);
            endLevelPadTriggers[1].gameObject.SetActive(true);

            RaycastHit hit;

            foreach (Transform endLevelPadTrigger in endLevelPadTriggers)
            {

                player1Ready = Physics.SphereCast(endLevelPadTrigger.position, radius, endLevelPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
                player2Ready = Physics.SphereCast(endLevelPadTrigger.position, radius, endLevelPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;

            }


            if (player1Ready || player2Ready)
            {

                Debug.Log("Everyone is ready!");

                string sceneName = SceneManager.GetActiveScene().name;

                if (sceneName == "Forest Level")
                    hostPlayer.completedForestLevel = true;
                else if (sceneName == "Water Level")
                    hostPlayer.completedWaterLevel = true;
                else if (sceneName == "Castle Level")
                    hostPlayer.completedCastleLevel = true;
                else if (sceneName == "Rock Level")
                    hostPlayer.completedRockLevel = true;

                NetworkManager.singleton.ServerChangeScene("Hub");
                SceneManager.LoadScene("Hub");

            }

        } else
        {

            endLevelPadTriggers[0].gameObject.SetActive(false);
            endLevelPadTriggers[1].gameObject.SetActive(false);

        }

    }
}
