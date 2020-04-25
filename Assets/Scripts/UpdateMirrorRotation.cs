using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateMirrorRotation : NetworkBehaviour
{

    public bool rotating = false;

    void Update()
    {
        
        if (rotating)
        {
            CmdSendRotation(transform.localRotation);
            Debug.Log("Sending Rotation");
        }

    }

    [Command]
    void CmdSendRotation(Quaternion r)
    {

        if (isServer)
        {
            transform.localRotation = r;
        }

        RpcUpdateRotation(r);

    }

    [ClientRpc]
    void RpcUpdateRotation(Quaternion r)
    {
        if (isClient && !rotating)
        {
            transform.localRotation = r;
            Debug.Log("Updating");

        }
    }

}
