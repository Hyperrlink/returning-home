using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomNetworkManager : NetworkManager
{

    public void StartHosting()
    {

        SetPort();
        NetworkManager.singleton.StartHost();

    }

    public void JoinGame()
    {

        SetPort();
        SetIPAddress();
        NetworkManager.singleton.StartClient();

    }

    private void SetIPAddress()
    {

        String ipAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;

    }

    void SetPort()
    {

        NetworkManager.singleton.networkPort = 1243;

    }

    

}
