using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BasicStateMachine : MonoBehaviour
{
    public enum State
    {
        StateA,
        StateB,
        StateC
    }
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.StateA;
        Debug.Log("Starting and current state is A");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            ChangeState();
        }
    }
    void ChangeState()
    {
        if (currentState == State.StateA)
        {
            currentState = State.StateB;
        }
        else if (currentState == State.StateB)
        {
            currentState = State.StateC;
        }
        else if (currentState == State.StateC)
        {
            currentState = State.StateA;
        }
        Debug.Log("Current state is" + currentState);
    }
}
