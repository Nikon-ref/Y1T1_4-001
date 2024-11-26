using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScoreUpdate : MonoBehaviour
{
    public int collected = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if statement to display on the console that if the prefab count from the spawner is 0 or less you will win the game and exit playmode
        if (this.transform.childCount <=0)
        {
            Debug.Log("You Lose!");
            EditorApplication.ExitPlaymode();
        } else
        {
            //otherwise it will display how many coins you have collected so far
            Debug.Log("Coins Collected: " + collected);
        }
    }
}
