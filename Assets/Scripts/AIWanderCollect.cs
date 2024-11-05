using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWanderCollect : MonoBehaviour
{
    GameObject[] coins;
    bool hastarget;
    float[] distances;
    GameObject target;
    GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        coins = manager.GetComponent<managegame>().coins; // Retrieves an array of coin GameObjects from the `managegame` script attached to the `manager`.
        distances = new float[coins.Length]; // Initializes the distances array with the same length as the `coins` array.
        hastarget = false; // Indicates that the AI does not have a target at the start.
    }

    // Update is called once per frame
    void Update()
    {
        if (!hastarget)//if the AI doesn't have a target
        {
            clearnull();//Removing all null objects from 
            bubble(coins);//Sorts the "coins" array based on the distance from the AI using a custom bubble sort
            hastarget = true;//setting target to true, AI now has a target
            target = coins[0];//Sets the first coin in the sorted array (the closest) as the target
        }
        else
        {
            movetoobject(target);
        }
    }
    void clearnull()
    {
        List<GameObject> coinslist = new List<GameObject>();//creating a temporary list to hold non-null coins
        for (int i = 0; i < coins.Length; i++)//loops through the coins array
        {
            if (coins[i] != null)//checks if the current coin is not null
            {
                coinslist.Add(coins[i]);//adds the non-null coin to the list
            }
        }
        coins = coinslist.ToArray();//converts the list back to an array and assigns it to "coins"
    }
    void bubble(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)//Outer loop iterates through the array
        {
            for (int j = 0; j < arr.Length -1; j++)//inner loop for comparing adjacent items
            {
                if (Vector3.Distance(arr[j].transform.position, transform.position) > Vector3.Distance(arr[j + 1].transform.position))
                {
                    GameObject temp = arr[j];//temporary storage for the current element
                    arr[j] = arr[j + 1];//swapping the current element with the next one
                    arr[j + 1] = temp;//assigning the current element to the next one
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)

    {
        if (other.tag == "Finish")//checking if the target he is colliding has "Finish" tag which is on the coin Prefab
        {
            hastarget = false;// Resets "hastarget" to indicate the AI needs a new target
            Destroy(other.gameObject);//Destroyrs the coin the AI collided with
        }
    }
}
