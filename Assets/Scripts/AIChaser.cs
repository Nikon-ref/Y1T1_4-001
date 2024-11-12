using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaser : MonoBehaviour
{
    Vector3 location;
    Vector3 direction;
    float speed = 1000f;
    private Rigidbody rb;
    GameObject EnemyAi;

    private enum AiChaserStates
    {
        StateRoaming,
        StateChasing,
        StateAttacking
    }
    AiChaserStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = AiChaserStates.StateRoaming;
        //SetRandomRoamingLocation(); //initial roaming target
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AiChaserStates.StateRoaming:
                Debug.Log("Currently roaming");
                location = (this.transform.position - transform.position);
                    rb.velocity = direction * speed * Time.deltaTime;
                break;
            //case AiChaserStates.StateChasing:
                Debug.Log("Currently chasing");
            //    if (Vector3.Distance(transform.position))
               
                break;
            case AiChaserStates.StateAttacking:
                Debug.Log("Currently attacking");
               
                break;

        }
    }
}
