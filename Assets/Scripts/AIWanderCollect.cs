using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWanderCollect : MonoBehaviour
{
    GameObject[] coins; // Array to store all the coins in the scene
    bool hastarget; // Bool to indicate if AI currently has a target
    GameObject target; // Current coin AI is targeting
    public GameObject coinSpawner; // Reference to CoinSpawner script
    float movespeed = 5000f;// Speed AI moves to target

    private Rigidbody rb; // Reference to Rigidbody component

    void Start()
    {
        
        rb = GetComponent<Rigidbody>(); // Get Rigidbody reference
        if (coinSpawner != null)
        {
            coins = coinSpawner.GetComponent<NewBehaviourScript>().GetCoins(); // Retrieve coins array from CoinSpawner
            hastarget = false; // AI starts without a target
        }
        else
        {
            Debug.LogError("CoinSpawner reference is not assigned!");
        }
    }

    void Update()
    { 
        coins = coinSpawner.GetComponent<NewBehaviourScript>().GetCoins();
        Debug.Log("AIWanderCollect Update() called."); // Log to confirm Update is running

        if (!hastarget) // If the AI doesn't have a target
        {
            clearnull(); // Remove null objects from the coins array

            if (coins.Length > 0)
            {
                bubble(coins); // Sorts coins by proximity to AI
                target = coins[0]; // Set the closest coin as the target
                hastarget = false; // Only set true if target is valid

                if (target != null)
                {
                    Debug.Log("New Target Acquired: " + target.name);
                    hastarget = true;
                }
                else
                {
                    Debug.Log("No target found, all coins may be null.");
                }
            }
        }
        else
        {
            movetoobject(target); // Move toward the target
        }
    }

    void clearnull()
    {
        List<GameObject> coinslist = new List<GameObject>(); // Temporary list to hold non-null coins
        for (int i = 0; i < coins.Length; i++)
        {
            if (coins[i] != null) // Check if the current coin is not null
            {
                coinslist.Add(coins[i]); // Add non-null coins to the list
            }
        }
        coins = coinslist.ToArray(); // Convert the list back to an array
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
                    GameObject temp = arr[j]; // Swap if current coin is farther
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) // If the AI collides with a coin
        {
            hastarget = false; // Reset target and select a new coin
            Destroy(other.gameObject); // Destroy the coin
            Debug.Log("Coin Collected: " + other.name);
        }
    }


    void movetoobject(GameObject target)
    {
        // Calculate direction and apply it directly to the Rigidbody's velocity
        Vector3 direction = (target.transform.position - transform.position).normalized;
        rb.velocity = direction * movespeed * Time.deltaTime;

        Debug.Log("Current Velocity: " + rb.velocity);
        Debug.Log("AI Position: " + transform.position + " | Target Position: " + target.transform.position);

    }

}


