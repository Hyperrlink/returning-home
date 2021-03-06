﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    public MouseLook mouseLook;
    public GameObject playerCamera;
    public GameObject playerBody;
    public Transform playerTransform;

    public Transform[] playerSpawnPos;

    Vector3 velocity;

    bool grounded;

    public float movementSpeed;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    public int playerNum;
    public string playerName;

    void Start()
    {

        gameObject.GetComponentInChildren<PauseMenuController>().playerNum = playerNum;

    }

    void Update()
    {

        if (!hasAuthority)
        {
            playerCamera.GetComponent<AudioListener>().enabled = false;
            playerCamera.SetActive(false);
            playerBody.SetActive(true);
            return;
        }

        playerCamera.SetActive(true);
        playerCamera.GetComponent<AudioListener>().enabled = true;
        playerBody.SetActive(false);

        Movement();

    }

    void Movement()
    {

        if (mouseLook.IsBlocked())
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            float speed = movementSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }

            controller.Move(move * speed * Time.deltaTime);

            GroundCheck();

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

    }

    void GroundCheck()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    }

}
