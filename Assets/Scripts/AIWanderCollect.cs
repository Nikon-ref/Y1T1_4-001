using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIWanderCollect : MonoBehaviour
{
    GameObject[] coins; // Array to store all the coins in the scene
    bool hastarget; // Bool to indicate if AI currently has a target
    GameObject target; // Current coin AI is targeting
    public GameObject coinSpawner; // Reference to CoinSpawner script
    public float movespeed = 5f; // Speed AI moves to target
    public GameObject aud;

    private NavMeshAgent navAgent; // Reference to NavMeshAgent component

    void Start()
    {
        // Initialize NavMeshAgent
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = movespeed;
        navAgent.stoppingDistance = 0.75f; // Small stopping distance for precise targeting
        navAgent.autoBraking = true;

        // Disable physics movement
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.isKinematic = true; // Disable physics control
        }

        if (coinSpawner != null)
        {
            coins = coinSpawner.GetComponent<NewBehaviourScript>().GetCoins();
            hastarget = false;
        }
        else
        {
            Debug.LogError("CoinSpawner reference is not assigned!");
        }
    }

    void Update()
    {
        if (!hastarget && target == null) // If the AI doesn't have a target
        {
            coins = coinSpawner.GetComponent<NewBehaviourScript>().GetCoins(); // Update coins array
            clearnull(); // Remove null objects from the coins array

            if (coins.Length > 0)
            {
                bubble(coins); // Sort coins by proximity
                target = coins[0]; // Choose the closest coin

                if (target != null)
                {
                    hastarget = true; // Mark as having a valid target
                    navAgent.SetDestination(GetNavMeshTarget(target)); // Navigate to the coin
                    Debug.Log("New Target Acquired: " + target.name);
                }
            }
        }
        else if (target != null && !navAgent.pathPending)
        {
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                // Stop if the AI reaches the coin
                target = null;
                hastarget = false;
                Debug.Log("Target Reached");
            }
        }
    }

    void clearnull()
    {
        Debug.Log("entering clearnull");
        List<GameObject> coinslist = new List<GameObject>();
        foreach (GameObject coin in coins)
        {
            if (coin != null) // Only keep valid coins
            {
                coinslist.Add(coin);
            }
        }
        coins = coinslist.ToArray(); // Convert back to array
    }

    void bubble(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length - 1; j++)
            {
                float distA = Vector3.Distance(arr[j].transform.position, transform.position);
                float distB = Vector3.Distance(arr[j + 1].transform.position, transform.position);

                if (distA > distB)
                {
                    GameObject temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) // Detect coin collection
        {
            Debug.Log("Coin collected: " + other.name);
            Destroy(other.gameObject);

            // Reset target
            target = null;
            hastarget = false;

            // Update coin array
            coins = coinSpawner.GetComponent<NewBehaviourScript>().GetCoins();
            clearnull();

            // Play collection sound
            aud.GetComponent<audmanager>().PlayPacManEatSound();
        }
    }

    private Vector3 GetNavMeshTarget(GameObject coin)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(coin.transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position; // Get the closest point on the NavMesh
        }
        else
        {
            Debug.LogWarning("Coin is not on NavMesh: " + coin.name);
            return coin.transform.position; // Use the coin position as fallback
        }
    }
}