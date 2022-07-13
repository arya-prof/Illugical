using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement playerMovement;
    
    private CharacterController _controller;

    [SerializeField] private float walkingSpeed;
    [SerializeField] private float lookingSpeed;
    private float speed;
    [SerializeField] private float gravity = -9.81f;

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
            _velocity.y = 0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        _controller.Move(move * speed * Time.deltaTime);

        _velocity.y += gravity * Time.deltaTime;

        _controller.Move(_velocity * Time.deltaTime);
    }
}