using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WaitUIController : MonoBehaviour
{

    public GameObject hostItems;
    public MouseLook mouseLook;

    void Start()
    {

        mouseLook = gameObject.GetComponentInChildren<MouseLook>();

    }

    void Update()
    {

        bool waiting = NetworkServer.connections.Count == 1;

        hostItems.SetActive(false);
        mouseLook.waiting = false;

    }
}
