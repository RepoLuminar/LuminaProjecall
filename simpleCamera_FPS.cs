using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleCamera_FPS : MonoBehaviour
{
     public float lookSpeed = 2f;
    public float maxLookUpAngle = 80f;
    public float maxLookDownAngle = 80f;

    private float rotationX = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
    }

    private void Update()
    {
        // Camera Look
        float lookHorizontal = Input.GetAxis("Mouse X") * lookSpeed;

        transform.Rotate(Vector3.up * lookHorizontal);

        // Limit vertical look rotation
        float lookVertical = Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX -= lookVertical;
        rotationX = Mathf.Clamp(rotationX, -maxLookDownAngle, maxLookUpAngle);

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
