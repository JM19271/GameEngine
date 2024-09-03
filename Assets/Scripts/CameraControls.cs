using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControls : MonoBehaviour
{
    public Transform player;
    public float mouseSensitivity = 100f;
    public float verticalRotationLimit = 80f;
    public Vector3 cameraOffset = new Vector3(0, 1.6f, 0);

    private float xRotation = 0f;

    private void Start()
    {
        // Lock the cursor to the game window and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Set the initial position of the camera relative to the player
        transform.position = player.position + cameraOffset;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        player.Rotate(Vector3.up * mouseX);

        // Rotate the camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalRotationLimit, verticalRotationLimit);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.position = player.position + player.TransformDirection(cameraOffset);
    }
}
