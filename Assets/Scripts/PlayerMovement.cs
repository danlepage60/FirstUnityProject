using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float groundsDistance = 0.4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;


    public LayerMask groundMask;
    
    float turnSmoothVelocity;

    
    Vector3 gravityVelocity;
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //use sphere colider to see if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundsDistance, groundMask);


        //resets gravity velocity when grounded 
        if(isGrounded && gravityVelocity.y < 0)
        {
            gravityVelocity.y = -2f;
        }

        //jump function
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        //set gravity and apply to controller
        gravityVelocity.y += gravity * Time.deltaTime;
        controller.Move(gravityVelocity * Time.deltaTime);


        //movement function based on mouse axis and allows player to rotate 
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f)* Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
