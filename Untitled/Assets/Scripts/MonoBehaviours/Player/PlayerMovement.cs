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
    private Vector3 _velocity;
    // Footsteps
    private bool isMoving = false;
    private IEnumerator playerFootstepsCoroutine;
    private string curMaterial = null;
    [SerializeField] private LayerMask footstepsLayermask; 

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
        if (References.Instance.freezWorld){
            // If its not already stopped
            if (playerFootstepsCoroutine != null){
                StopFootstepSFX();
            }
            return;
        }

        bool isGrounded = _controller.isGrounded;
        if (isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // If allowed to play the footsteps sfx
        if (move != Vector3.zero) { isMoving = true; } else { isMoving = false; }
        if (!_controller.isGrounded) { isMoving = false; }

        if (isMoving && playerFootstepsCoroutine == null){
            // Start playing
            StartFootstepSFX();
        }
        if (!isMoving && playerFootstepsCoroutine != null){
            // Stop the sound
            StopFootstepSFX();
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

    private void StartFootstepSFX(){
        playerFootstepsCoroutine = References.Instance.PlayFootsteps(curMaterial, 0.2f, 0.2f, 0f);
        StartCoroutine(playerFootstepsCoroutine);
    }
    private void StopFootstepSFX(){
        References.Instance.footstepsSource.Stop();
        StopCoroutine(playerFootstepsCoroutine);
        playerFootstepsCoroutine = null;
    }

    // Material change
    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (footstepsLayermask == (footstepsLayermask | (1 << hit.gameObject.layer))) {
            // The first frame we are not moving so it will kinda bug and skip the first frame for safety
            if (curMaterial != hit.gameObject.tag && isMoving){
                curMaterial = hit.gameObject.tag;
                StopFootstepSFX();
                StartFootstepSFX();
            }
            curMaterial = hit.gameObject.tag;
        }
        
    }
}