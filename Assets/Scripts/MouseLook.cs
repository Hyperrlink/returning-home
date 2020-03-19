using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public bool interacting = false;
    public bool paused = false;
    public bool waiting = false;

    public float mouseSensitivity;
    float xRotation = 0f;

    public Transform playerBody;
    public Camera cam;

    void Start()
    {

        cam = GetComponent<Camera>();
        cam.depthTextureMode = DepthTextureMode.Depth;

    }

    void Update()
    {

        if (IsBlocked())
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

    }

    public bool IsBlocked()
    {

        return !interacting && !paused && !waiting;

    }

}
