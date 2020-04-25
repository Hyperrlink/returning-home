using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class EndLevel : NetworkBehaviour
{

    public bool triggered = false;

    public Transform[] endLevelPadTriggers;

    public bool player1Ready = false;
    public bool player2Ready = false;

    public float radius;
    public float maxDistance;

    public LayerMask playerLayer;

    void Start()
    {

        GameObject[] temp = GameObject.FindGameObjectsWithTag("EndLevelPadTrigger");

        endLevelPadTriggers = new Transform[2];

        if (temp.Length == 0)
        {
            Debug.Log("Shit");
        }
        else
        {
            endLevelPadTriggers[0] = temp[0].transform;
            endLevelPadTriggers[1] = temp[1].transform;
        }

    }

    void Update()
    {

        if (!hasAuthority)
        {
            return;
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
