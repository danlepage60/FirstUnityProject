using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private float zMovement;
    private Rigidbody rb;
    private bool jump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        zMovement = Input.GetAxis("Horizontal");


    }

    void FixedUpdate()
    {
        if (jump)
        {
            Debug.Log("jump");
            rb.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jump = false;
        }

        rb.velocity = new Vector3(zMovement, rb.velocity.y, 0);

    }
}
