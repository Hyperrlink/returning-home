using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    Vector3 velocity;

    bool grounded;

    public float movementSpeed;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight;

    void Start()
    {
        
    }

    void Update()
    {

        Movement();

    }

    void Movement() {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * movementSpeed * Time.deltaTime);

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
