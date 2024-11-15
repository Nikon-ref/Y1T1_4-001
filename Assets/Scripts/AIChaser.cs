using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaser : MonoBehaviour
{
    Vector3 location;
    float speed = 30f; // Speed of movement
    private Rigidbody rb;
    public GameObject EnemyAi; // Target to chase

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
        else if (EnemyAi != null && Vector3.Distance(transform.position, EnemyAi.transform.position) < 30f)
        {
            currentState = AiChaserStates.StateChasing;
        }
    }

    void ChasingBehavior()
    {
        Debug.Log("Currently chasing");
        if (EnemyAi != null)
        {
            Vector3 direction = (EnemyAi.transform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * speed * Time.fixedDeltaTime;
            rb.MovePosition(newPosition);

            // Transition to attacking if close enough to the target
            if (Vector3.Distance(transform.position, EnemyAi.transform.position) < 1.5f)
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
        if (EnemyAi != null && Vector3.Distance(transform.position, EnemyAi.transform.position) > 2f)
        {
            currentState = AiChaserStates.StateChasing;
        }
    }
}
