using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // variables
     public float moveSpeed;
     public float walkSpeed;
     public float runSpeed;
    
    // the direction we are moving in
    private Vector3 moveDirection;

    //keeps track of up and down position in the move direction/keeps track of gravity and jumping
    private Vector3 velocity;

    // keeps track if the player is on the ground
    public bool isGrounded;
    public float groundCheckDistance;
    public LayerMask groundMask;
    public float gravity;
    public float jumpHeight;



    // references

    // reference to our character controller on our player object
    private CharacterController controller;
    private Animator anim;

    // this function gets called whenever the game starts
    private void Start()
    {   
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // this function is called each frame
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Gravity Check
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);
        
        // stops applying gravity when we are grounded
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // using Z axis because it is forward and backward axis
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);
        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded)
        {
        
        if(moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            Walk();
        }
        else if(moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            Run();
        }
        else if(moveDirection == Vector3.zero)
        {
            Idle();
        }

            moveDirection *= moveSpeed; 

            if(Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
    }
        

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }

    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }

    private void Jump()
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}