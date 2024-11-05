using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour
{
    private CharacterController _characterController;
    public float movementSpeed = 10f;
    private Rigidbody rb;

    private void Start()
    {
        //rigid body reference
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        // A and D
        float moveHorizontal = Input.GetAxis("Horizontal");

        // W and S
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 8, moveVertical) * movementSpeed * Time.deltaTime;
        
        // movement vector pointing direction
        movement = transform.TransformDirection(movement);

        //applying the movement vector to the rigidbody
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }
}