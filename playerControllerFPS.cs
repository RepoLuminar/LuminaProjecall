using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerFPS : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float lookSpeed = 2f;
    public float maxLookUpAngle = 80f;
    public float maxLookDownAngle = 80f;
    public float gravity = 9.81f;
    public float jumpHeight = 6f;

    private CharacterController controller;
    private Transform playerCamera;
    private float rotationX = 0f;
    private Vector3 playerVelocity;
    private bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = Camera.main.transform;
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = controller.isGrounded;

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Player Jump
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * 2f * gravity);
        }

        // Apply gravity
        playerVelocity.y -= 6f * gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Camera Look (Rotasi sumbu Y)
        float lookHorizontal = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * lookHorizontal * lookSpeed);

        // Rotasi sumbu Y karakter mengikuti kamera
        float lookVertical = Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX -= lookVertical;
        rotationX = Mathf.Clamp(rotationX, -maxLookDownAngle, maxLookUpAngle);
        playerCamera.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
