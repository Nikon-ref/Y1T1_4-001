using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float amplitude = 0.25f;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        //initial position
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //evrtical movement using Mathf.Sin
        //Time.time for more smooth movement
        float verticalMovement = Mathf.Sin(Time.time * moveSpeed) * amplitude;

        //vertical movement added to its initial position
        Vector3 newPosition = startPos + Vector3.up * verticalMovement;
        transform.position = newPosition;
    }
}
