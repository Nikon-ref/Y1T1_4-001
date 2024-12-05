using System.Collections;
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
            EndGame();
        }
        // Check if all enemies are dead
        else if (enemyParent.childCount <= 0)
        {
            Debug.Log("You Win! All enemies are dead.");
            EndGame();
        }
        else
        {
            // Display how many coins have been collected so far
            Debug.Log("Coins Collected by the enemies: " + collected);
        }
    }

    void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEngine;

//public class ScoreUpdate : MonoBehaviour
//{
//    public int collected = 0;
//    // Start is called before the first frame update
//    void Start()
//    {
//
//    }

// Update is called once per frame
//    void Update()
//    {
//        //if statement to display on the console that if the prefab count from the spawner is 0 or less you will win the game and exit playmode
//        if (this.transform.childCount <= 0)
//        {
//            Debug.Log("You Win!");
//            EditorApplication.ExitPlaymode();
//        }
//        else
//        {
//            //otherwise it will display how many coins you have collected so far
//           Debug.Log("Coins Collected: " + collected);
//        }
//    }
//}
