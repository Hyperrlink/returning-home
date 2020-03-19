using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
<<<<<<< HEAD
    public MouseLook mouseLook;
    public GameObject playerCamera;
    public Transform playerTransform;

    public Transform[] playerSpawnPos;

    public GameObject parent;
=======
>>>>>>> parent of 449859a... 0.0.6

    Vector3 velocity;

    bool grounded;

    public float movementSpeed;
    public float sprintSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public int playerNum;

    void Start()
    {

        

    }

    void Update()
    {

        if (!hasAuthority)
        {
            playerCamera.SetActive(false);
            return;
        }

        playerCamera.SetActive(true);

        Movement();

    }

    void Movement()
    {

<<<<<<< HEAD
        if (mouseLook.IsBlocked())
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
=======
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
>>>>>>> parent of 449859a... 0.0.6

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

    void GroundCheck()
    {

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

    }

}
