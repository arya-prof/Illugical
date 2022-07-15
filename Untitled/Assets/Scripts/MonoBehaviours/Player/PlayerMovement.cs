using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement playerMovement;
    
    private CharacterController _controller;

    public float walkingSpeed;
    public float lookingSpeed;
    public float speed;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float gravity = -9.81f;
    private bool isMoving = false;

    private Vector3 _velocity;

    private void Awake()
    {
        playerMovement = this;
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();

        speed = walkingSpeed;
    }
    
    private void Update()
    {
        bool isGrounded = _controller.isGrounded;
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (move != Vector3.zero){
            isMoving = true;
        }
        else{
            isMoving = false;
        }

        if (isMoving && !References.Instance.playFootsteps){
            StartCoroutine(References.Instance.PlayFootsteps("wood", 0.2f, 0.2f, 0.2f));
        }
        if (!isMoving){
            References.Instance.StopFootsteps();
        }

        _controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown("space") && isGrounded)
        {
            if (HandController.handController.HandControlleAble)
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }

        _velocity.y += gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}