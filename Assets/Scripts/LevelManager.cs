using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LevelManager : NetworkBehaviour
{

    public Transform[] forestPadTriggers;

    public bool player1Ready = false;
    public bool player2Ready = false;

    public float radius;
    public float maxDistance;

    public LayerMask playerLayer;

    public string levelName = "";

    void Start()
    {

        GameObject[] temp = GameObject.FindGameObjectsWithTag("ForestLevelPadTrigger");

        for (int i = 0; i < temp.Length; i++) {

            forestPadTriggers[i] = temp[i].GetComponent<Transform>(); 

        }

    }

    void FixedUpdate()
    {

        if (!hasAuthority)
        {
            return;
        }

        RaycastHit hit;

        foreach (Transform forestPadTrigger in forestPadTriggers)
        {

            player1Ready = Physics.SphereCast(forestPadTrigger.position, radius, forestPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 1;
            player2Ready = Physics.SphereCast(forestPadTrigger.position, radius, forestPadTrigger.transform.up, out hit, maxDistance, playerLayer, QueryTriggerInteraction.UseGlobal) && hit.collider.gameObject.GetComponent<PlayerMovement>().playerNum == 2;
            levelName = "Forest Level";

        }


        if (player1Ready || player2Ready)
        {

            Debug.Log("Everyone is ready!");

            NetworkManager.singleton.ServerChangeScene(levelName);
            SceneManager.LoadScene(levelName);

        }

    }

}
