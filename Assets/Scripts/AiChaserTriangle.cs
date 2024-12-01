using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIChaser : MonoBehaviour
{
    // Roaming points (hardcoded coordinates handpicked)
    private readonly Vector3 pointA = new Vector3(43.86f, 5.4823f, -85.52f);
    private readonly Vector3 pointB = new Vector3(-17.86f, 5.4823f, 19.74f);
    private readonly Vector3 pointC = new Vector3(92.3f, 5.4823f, 82.02f);

    public float roamingSpeed = 3.5f; // Speed while roaming
    public float chasingSpeed = 5.5f; // Speed while chasing
    public float stoppingDistance = 1.5f; // Distance to stop while attacking

    private NavMeshAgent navAgent; // Reference to NavMeshAgent
    private GameObject[] aiCollectors; // Array of potential targets
    private GameObject currentTarget; // The current target AICollector
    private Vector3[] roamPoints; // Array of the three roaming points
    private int currentRoamIndex = 0; // Index to track current roam point

    private enum AiChaserStates
    {
        StateRoaming,
        StateChasing,
        StateAttacking
    }
    AiChaserStates currentState;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        // Initialize roaming points array
        roamPoints = new Vector3[] { pointA, pointB, pointC };

        // Start in roaming state
        currentState = AiChaserStates.StateRoaming;
        SetRoamingDestination();
    }

    void Update()
    {
        UpdateClosestTarget(); // Check for nearby targets

        switch (currentState)
        {
            case AiChaserStates.StateRoaming:
                RoamingBehavior();
                break;
            case AiChaserStates.StateChasing:
                ChasingBehavior();
                break;
            case AiChaserStates.StateAttacking:
                AttackingBehavior();
                break;
        }
    }

    void SetRoamingDestination()
    {
        // Set NavMeshAgent to move to the next point in the loop
        navAgent.speed = roamingSpeed;
        navAgent.SetDestination(roamPoints[currentRoamIndex]);
    }

    void RoamingBehavior()
    {
        Debug.Log("Currently roaming");

        // Check if the AI has reached the current roam point
        if (!navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // Move to the next point in the array
            currentRoamIndex = (currentRoamIndex + 1) % roamPoints.Length; // Loop back to the first point
            SetRoamingDestination();
        }

        // Switch to chasing state if a target is found
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) < 30f)
        {
            currentState = AiChaserStates.StateChasing;
            navAgent.speed = chasingSpeed;
        }
    }

    void ChasingBehavior()
    {
        if (currentTarget != null)
        {
            navAgent.SetDestination(currentTarget.transform.position);

            // Switch to attacking state if close to the target
            if (!navAgent.pathPending && navAgent.remainingDistance <= stoppingDistance)
            {
                currentState = AiChaserStates.StateAttacking;
            }
        }
        else
        {
            // Return to roaming if no target
            currentState = AiChaserStates.StateRoaming;
            SetRoamingDestination();
        }
    }

    void AttackingBehavior()
    {
        navAgent.ResetPath(); // Stop the agent

        // Example: Transition back to chasing if the target moves out of range
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > stoppingDistance)
        {
            currentState = AiChaserStates.StateChasing;
            navAgent.speed = chasingSpeed;
        }
    }

    void UpdateClosestTarget()
    {
        aiCollectors = GameObject.FindGameObjectsWithTag("Player"); // Find all potential targets

        float closestDistance = Mathf.Infinity;
        GameObject closestCollector = null;

        foreach (GameObject collector in aiCollectors)
        {
            if (collector != null)
            {
                float distance = Vector3.Distance(transform.position, collector.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollector = collector;
                }
            }
        }

        currentTarget = closestCollector; // Update the current target
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class AIChaser : MonoBehaviour
//{
    //Vector3 location;
    //float speed = 30f; // Speed of movement
    //private Rigidbody rb;
    //private GameObject[] aiCollectors; // Array of all AICollector objects
    //private GameObject currentTarget; // Current target AICollector

    //private enum AiChaserStates
    //{
        //StateRoaming,
        //StateChasing,
        //StateAttacking
    //}
    //AiChaserStates currentState;

    // Roaming boundaries
    //public Vector2 xRange = new Vector2(-90f, 100f);
    //public Vector2 zRange = new Vector2(-105f, 80f);

    //void Start()
    //{
        //rb = GetComponent<Rigidbody>();
        //currentState = AiChaserStates.StateRoaming;
        //SetRandomRoamingLocation(); // Initial roaming target
    //}

    ///void FixedUpdate() // Use FixedUpdate for physics-based movement
    //{
        //UpdateClosestTarget(); // Update the closest target at the start of each FixedUpdate
        //switch (currentState)
        //{
            //case AiChaserStates.StateRoaming:
                //RoamingBehavior();
                //break;
            //case AiChaserStates.StateChasing:
                //ChasingBehavior();
                //break;
            //case AiChaserStates.StateAttacking:
                //AttackingBehavior();
                //break;
        //}
    //}

    //void SetRandomRoamingLocation()
    //{
        //float randomX = Random.Range(xRange.x, xRange.y);
        //float randomZ = Random.Range(zRange.x, zRange.y);
        //location = new Vector3(randomX, transform.position.y, randomZ);

        // Ensure the random location is not too close
        //if (Vector3.Distance(transform.position, location) < 5f)
        //{
            //SetRandomRoamingLocation();
        //}
        //Debug.Log("New Roaming Location: " + location);
    //}

    //void RoamingBehavior()
    //{
        //Debug.Log("Currently roaming");
        //Vector3 direction = (location - transform.position).normalized;
        //Vector3 newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
        //rb.MovePosition(newPosition);

        // Check if close to the roaming target location
        //if (Vector3.Distance(transform.position, location) < 1f)
        //{
           // SetRandomRoamingLocation(); // Set a new random location
        //}
        //else if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) < 30f)
        //{
            //currentState = AiChaserStates.StateChasing;
        //}
    //}

    //void ChasingBehavior()
    //{
        //Debug.Log("Currently chasing");
        //if (currentTarget != null)
        //{
            //Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            //Vector3 newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
            //rb.MovePosition(newPosition);

            // Transition to attacking if close enough to the target
            //if (Vector3.Distance(transform.position, currentTarget.transform.position) < 1.5f)
            //{
                //currentState = AiChaserStates.StateAttacking;
            //}
        //}
        //else
        //{
            //currentState = AiChaserStates.StateRoaming; // Return to roaming if no target
        //}
    //}
    //void AttackingBehavior()
    //{
        //Debug.Log("Currently attacking");
        ///rb.velocity = Vector3.zero; // Stop movement during attack

        // Example logic: Return to chasing if target moves out of range
        //if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > 2f)
        //{
           // currentState = AiChaserStates.StateChasing;
        //}
    //}

    //void UpdateClosestTarget()
    //{
        //aiCollectors = GameObject.FindGameObjectsWithTag("Player"); // Refresh the list of AICollectors

        //float closestDistance = Mathf.Infinity; // Start with a very large distance
        //GameObject closestCollector = null;

        //foreach (GameObject collector in aiCollectors)
        //{
            //if (collector != null) // Ensure the collector is valid
            //{
                //float distance = Vector3.Distance(transform.position, collector.transform.position);

                //if (distance < closestDistance) // Check if this collector is closer
                //{
                    //closestDistance = distance;
                    //closestCollector = collector;
                //}
            //}
        //}

        //currentTarget = closestCollector; // Update the current target
    //}
//}

