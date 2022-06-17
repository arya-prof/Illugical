
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravityScale;
    public float movementSpeed = 0.5f;
    private Rigidbody rb;
    public Transform groundCheck;
    public float groundDistance = 0.4f; // max distance from ground after that it will say you are grounded
    public LayerMask groundMask; // layers it can have collision with it
    public float jumpHeight = 3.0f;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void Update(){
        // jump system
        isGrounded = Physics.Raycast(groundCheck.position ,Vector3.down ,groundDistance ,groundMask);
        

        if (Input.GetButtonDown("Jump") && isGrounded){
            rb.AddForce(Vector3.up * jumpHeight);
            isGrounded = false;
        }

    }
    void FixedUpdate()
    {
        // movement system
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * movementSpeed;
        
        rb.AddForce(new Vector3(move.x-rb.velocity.x, 0 ,move.z-rb.velocity.z), ForceMode.VelocityChange);
    }

    // private void OnCollisionEnter(Collision other) {
    //     if (References.Instance.wallsLayer == (References.Instance.wallsLayer | (1 << other.gameObject.layer))){
    //         allowedToMove = false;
    //     }
    // }
    // private void OnCollisionExit(Collision other) {
    //     if (References.Instance.wallsLayer == (References.Instance.wallsLayer | (1 << other.gameObject.layer))){
    //         allowedToMove = true;
    //     }
    // }
}