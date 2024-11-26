using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgentMover : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;
    private NavMeshAgent agent;
    
    void Start()
    {
        //Finds and stores the NavMeshAgent component to the same GameObject
        //This ensures the script can control the agent
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if a target transform has been assigned to avoid errors
        //if the target is missing or unassigned
        if (target != null)
        {
            //Sets the agent's destination to the target's position
            //The NavMeshAgent calculates and follows the path to this position
            agent.SetDestination(target.position);
        }
    }
}
