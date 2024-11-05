using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    //target object
    public Transform target;

    //variables for position and sense
    public float distance = 5.0f;
    public float sensitivity = 2.0f;
    public float heightOffset = 1.5f;

    //rotation variables
    public float rotationX = 0.0f;
    public float rotationY = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        //locking and hiding cursor for camera control (right click on unity to have mouse disappear)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //Start rotation to current camera angles
        rotationX = transform.eulerAngles.x;
        rotationY = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        //modify rotation of opposite axis, because of how rotation works around an axis, the up and down movement is reversed so it feels natural
        rotationY += mouseX;
        rotationX -= mouseY;

        //prevent camera inverting
        rotationX = Mathf.Clamp(rotationX, -90, 90);
    }
    //Initially tried to introduce the third person camera view without late update and it would look jittery, now it's fixed
    void LateUpdate()

    {
        // Update the camera's rotation based on the input
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);

        // Set the camera's position relative to the target
        Vector3 positionOffset = new Vector3(0, heightOffset, -distance);
        transform.position = target.position + rotation * positionOffset;

        // Ensure the camera is always looking at the target
        transform.LookAt(target.position + Vector3.up * heightOffset);
    }
}