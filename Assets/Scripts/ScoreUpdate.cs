using System.Collections;
using UnityEditor;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public int collected = 0; // Number of collected coins
    public Transform enemyParent; // Reference to the parent object containing all enemies
    public Transform coinParent;  // Reference to the parent object containing all coins

    void Update()
    {
        // Check if all coins are collected
        if (coinParent.childCount <= 0)
        {
            Debug.Log("You Lose! All coins have been collected.");
            EditorApplication.ExitPlaymode();
        }
        // Check if all enemies are dead
        else if (enemyParent.childCount <= 0)
        {
            Debug.Log("You Win! All enemies are dead.");
            EditorApplication.ExitPlaymode();
        }
        else
        {
            // Display how many coins have been collected so far
            Debug.Log("Coins Collected by the enemies: " + collected);
        }
    }
}
