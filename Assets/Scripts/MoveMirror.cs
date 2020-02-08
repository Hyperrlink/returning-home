using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MoveMirror : NetworkBehaviour
{

    public Transform cameraPos;
    public Text pressE;
    public MouseLook ml;

    [SyncVar] private Quaternion syncMirrorRotation;

    public float maxDistance = 10f;
    public float mouseSensitivity;
    float xRotation = 0f;
    float yRotation = 0f;

    void Start()
    {

        pressE.enabled = false;

    }

    void Update()
    {

        if (!hasAuthority)
        {
            return;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Ray pointer = new Ray(cameraPos.position, cameraPos.forward);
        Transform mirror = null;

        if (mirror != null)
        {
            RpcTransmitRotation(mirror.localRotation);
        }

        RaycastHit hit;
        if (Physics.Raycast(pointer, out hit, maxDistance) && (hit.collider.gameObject.tag == "MirrorStand" || hit.collider.gameObject.tag == "Mirror"))
        {
            pressE.enabled = true;

            if (Input.GetKey(KeyCode.E))
            {
                ml.interacting = true;

                if (hit.collider.gameObject.tag == "MirrorStand")
                    mirror = hit.collider.gameObject.transform.GetChild(0);
                else
                    mirror = hit.collider.gameObject.transform;

                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, -70f, 70f);

                yRotation -= mouseX;
                yRotation = Mathf.Clamp(yRotation, -70f, 70f);

                mirror.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);

            } else if (Input.GetKeyUp(KeyCode.E))
            {
                ml.interacting = false;
            }

        }
        else
        {
            pressE.enabled = false;
            ml.interacting = false;
        }

    }

    [Command]
    void CmdProvideRotationToServer(Quaternion mirrorRotation)
    {

        syncMirrorRotation = mirrorRotation;

    }

    [ClientRpc]
    void RpcTransmitRotation(Quaternion mirrorRotation)
    {

        CmdProvideRotationToServer(mirrorRotation);

    }

}
