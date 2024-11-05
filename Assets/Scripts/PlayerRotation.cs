using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    //mouse input
    public float mouseSensitivity = 2.0f;
    
    //handling rotation on the Y axis
    private float verticalRotation = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //ROTATIONS
        //right and left movement of the mouse and scale it by the sensitivity
        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;

        //apply the movement to the player's rotation AROUND the Y axis
        transform.Rotate(0, horizontalRotation, 0);


        float verticalMouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity;



        //manipulate the vertical rotation using Mouse Y, scale it based on mouseSensitivity
        // BEFORE verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= verticalMouseMovement;
        //.Clamp rotation values so that camera """can't clip through the floor""" and you can't go beyond looking 90^ Ow
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        //transform of the sceme's main camera and adjust vertical rotation
        //BEFORE Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        //Apply vertical rotation to the camera's local x axis

        //reapplied Camera.main
     Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //horizontal rotation should affect player's body//vertical rotation only the camera
    } 
}//Before the above code I had this instead on void Update
//void Update()
//{
    // [Text] Get mouse input for horizontal (X) and vertical (Y) movement
    //float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
    //float verticalMouseMovement = Input.GetAxis("Mouse Y") * mouseSensitivity;

    // [text] Apply horizontal rotation to the player's body (Y-axis)
    //transform.Rotate(0, horizontalRotation, 0);

    // [text] Update vertical rotation (clamp between -90 and 90 degrees to prevent flipping)
    // verticalRotation -= verticalMouseMovement;
    //verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

    // [text] Apply vertical rotation to the camera (X-axis rotation only)
    //playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
//} I think this could have worked the same way, the issue we found was on the "character controller" in unity which was enabled as I had toyed with that before making the character controller scripts since I didn't know how to make scripts.