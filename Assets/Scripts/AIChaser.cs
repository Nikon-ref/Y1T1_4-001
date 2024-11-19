using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaser : MonoBehaviour
{
    Vector3 location;
    float speed = 30f; // Speed of movement
    private Rigidbody rb;
    private GameObject[] aiCollectors; // Array of all AICollector objects
    private GameObject currentTarget; // Current target AICollector

    private enum AiChaserStates
    {
        StateRoaming,
        StateChasing,
        StateAttacking
    }
    AiChaserStates currentState;

    // Roaming boundaries
    public Vector2 xRange = new Vector2(-90f, 100f);
    public Vector2 zRange = new Vector2(-105f, 80f);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentState = AiChaserStates.StateRoaming;
        SetRandomRoamingLocation(); // Initial roaming target
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        UpdateClosestTarget(); // Update the closest target at the start of each FixedUpdate
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

    void SetRandomRoamingLocation()
    {
        float randomX = Random.Range(xRange.x, xRange.y);
        float randomZ = Random.Range(zRange.x, zRange.y);
        location = new Vector3(randomX, transform.position.y, randomZ);

        // Ensure the random location is not too close
        if (Vector3.Distance(transform.position, location) < 5f)
        {
            SetRandomRoamingLocation();
        }
        Debug.Log("New Roaming Location: " + location);
    }

    void RoamingBehavior()
    {
        Debug.Log("Currently roaming");
        Vector3 direction = (location - transform.position).normalized;
        Vector3 newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        // Check if close to the roaming target location
        if (Vector3.Distance(transform.position, location) < 1f)
        {
            SetRandomRoamingLocation(); // Set a new random location
        }
        else if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) < 30f)
        {
            currentState = AiChaserStates.StateChasing;
        }
    }

    void ChasingBehavior()
    {
        Debug.Log("Currently chasing");
        if (currentTarget != null)
        {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            // Transition to attacking if close enough to the target
            if (Vector3.Distance(transform.position, currentTarget.transform.position) < 1.5f)
            {
                currentState = AiChaserStates.StateAttacking;
            }
        }
        else
        {
            currentState = AiChaserStates.StateRoaming; // Return to roaming if no target
        }
    }

    void AttackingBehavior()
    {
        Debug.Log("Currently attacking");
        rb.velocity = Vector3.zero; // Stop movement during attack

        // Example logic: Return to chasing if target moves out of range
        if (currentTarget != null && Vector3.Distance(transform.position, currentTarget.transform.position) > 2f)
        {
            currentState = AiChaserStates.StateChasing;
        }
    }

    void UpdateClosestTarget()
    {
        aiCollectors = GameObject.FindGameObjectsWithTag("Player"); // Refresh the list of AICollectors

        float closestDistance = Mathf.Infinity; // Start with a very large distance
        GameObject closestCollector = null;

        foreach (GameObject collector in aiCollectors)
        {
            if (collector != null) // Ensure the collector is valid
            {
                float distance = Vector3.Distance(transform.position, collector.transform.position);

                if (distance < closestDistance) // Check if this collector is closer
                {
                    closestDistance = distance;
                    closestCollector = collector;
                }
            }
        }

        currentTarget = closestCollector; // Update the current target
    }
}

