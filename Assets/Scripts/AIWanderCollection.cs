using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AIWanderCollection : MonoBehaviour
{
    public GameObject coinSpawner; // Reference to the coin spawner
    private NavMeshAgent navAgent; // Reference to the NavMeshAgent
    private GameObject targetCoin; // The current target coin
    private GameObject[] coins; // Array to store all coins
    public GameObject aud;
    public float movementSpeed = 5f;

    void Start()
    {
        // Get the NavMeshAgent component
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = movementSpeed;

        // Find all coins in the scene from the spawner
        UpdateCoinList();
    }

    void Update()
    {
        // If there's no target coin, find the closest one
        if (targetCoin == null)
        {
            UpdateCoinList(); // Refresh the coin list
            targetCoin = GetClosestCoin(); // Find the nearest coin

            if (targetCoin != null)
            {
                navAgent.SetDestination(targetCoin.transform.position); // Set destination to the target coin
            }
        }

        // Check if the AI has reached the target coin
        if (targetCoin != null && navAgent.remainingDistance <= navAgent.stoppingDistance && !navAgent.pathPending)
        {
            // Reached the coin; wait for OnTriggerEnter to handle collection
            navAgent.ResetPath(); // Stop the agent for now
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is a coin
        if (other.CompareTag("Finish")) // Replace "Finish" with your coin's tag
        {
            Debug.Log("Collected coin: " + other.name);
            Destroy(other.gameObject); // Destroy the collected coin
            targetCoin = null; // Clear the target to find the next coin
            // Play collection sound
            aud.GetComponent<audmanager>().PlayPacManEatSound();
        }
    }


    private void UpdateCoinList()
    {
        // Fetch all coins from the coin spawner
        coins = coinSpawner.GetComponent<NewBehaviourScript>().GetCoins();
    }

    private GameObject GetClosestCoin()
    {
        GameObject closest = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject coin in coins)
        {
            if (coin != null)
            {
                float distance = Vector3.Distance(transform.position, coin.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closest = coin;
                }
            }
        }

        return closest; // Return the closest coin, or null if none found
    }
}
