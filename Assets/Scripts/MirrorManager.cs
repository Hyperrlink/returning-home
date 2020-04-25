using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class MirrorManager : NetworkBehaviour
{

    /*GameObject[] mirrors;

    void Start()
    {

        DontDestroyOnLoad(gameObject);

    }
 
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        mirrors = GameObject.FindGameObjectsWithTag("Mirror");

        foreach (GameObject mirror in mirrors)
        {

            NetworkIdentity ni = mirror.GetComponent<NetworkIdentity>();
            ni.AssignClientAuthority(mirror);

        }

    }*/

}
