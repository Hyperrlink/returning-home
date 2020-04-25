using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

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

        NetworkManager.singleton.networkPort = 7777;

    }

    private void OnLevelWasLoaded(int level)
    {
        
        if(level == 0)
        {
            SetupMenuSceneButtons();
        } else
        {
            SetupOtherSceneButtons();
        }

    }

    void SetupMenuSceneButtons()
    {

        return;
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartHost").GetComponent<Button>().onClick.AddListener(StartHosting);

        GameObject.Find("ButtonStartClient").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonStartClient").GetComponent<Button>().onClick.AddListener(JoinGame);

    }

    void SetupOtherSceneButtons()
    {
        return;
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopServer);

        GameObject.Find("ButtonOtherDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonOtherDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopClient);

    }

}
