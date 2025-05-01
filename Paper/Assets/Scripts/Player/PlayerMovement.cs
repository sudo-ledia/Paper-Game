using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 5f;  
    public float jumpForce = 5f; 
    public float gravity;
    public float groundRay;
    public float sphereRadius = 0.3f;
    public bool isGrounded = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Player Movement
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector3.forward * speed, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(Vector3.left * speed, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(Vector3.back * speed, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Force);
        }

        // Ground detection using SphereCast
        float rayLength = groundRay;
        
        Vector3 origin = transform.position;
        Vector3 direction = Vector3.down;

        RaycastHit hit;
        bool isHit = Physics.SphereCast(origin, sphereRadius, direction, out hit, rayLength);
        Debug.DrawRay(origin, rayLength * direction, Color.red);
        isGrounded = isHit;

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
        }
        else if (!isGrounded)
        {
            rb.AddForce(0, gravity, 0);
        }

        // Velocity limiter
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        

    }
}
