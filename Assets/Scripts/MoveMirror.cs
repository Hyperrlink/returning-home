using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveMirror : MonoBehaviour
{

    public Transform cameraPos;
    public Text pressE;
    public MouseLook ml;
    public PlayerMovement pm;

<<<<<<< HEAD
    [SyncVar(hook = "OnRotationChanged")]
    private Quaternion syncMirrorRotation;

    Transform mirror = null;

    private Quaternion mirrorRotation;

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

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        Ray pointer = new Ray(cameraPos.position, cameraPos.forward);
<<<<<<< HEAD
        
        RaycastHit hit;
        if (Physics.Raycast(pointer, out hit, maxDistance) && (hit.collider.gameObject.tag == "MirrorStand" || hit.collider.gameObject.tag == "Mirror"))
        {
            pressE.enabled = true;

            if (Input.GetKey(KeyCode.E))
            {
                ml.interacting = true;

                Transform mirror;

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

        TransmitRotation(mirrorRotation);

    }

<<<<<<< HEAD
    void OnRotationChanged(Quaternion newRotation)
    {

        if (mirror == null)
            return;

        Debug.Log("Got new rotation");
        mirror.localRotation = newRotation;

    }

    [ClientCallback]
    void TransmitRotation(Quaternion mirrorRotation)
    {

        if (hasAuthority)
        {
            CmdProvideRotationToServer(mirrorRotation);
        }

    }

    [Command]
    void CmdProvideRotationToServer(Quaternion mirrorRotation)
    {

        syncMirrorRotation = mirrorRotation;

    }

}
