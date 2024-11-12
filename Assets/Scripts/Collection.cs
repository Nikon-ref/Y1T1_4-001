using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : MonoBehaviour
{
    private GameObject spawner;
    // Start is called before the first frame update
    void Start()
    {
        //using tags so it's easier to find all objects that are children of the "spawner"
        spawner = GameObject.FindGameObjectWithTag("Spawner");
    }

    private void OnCollisionEnter(Collision collision) //Use trigger if you want the player to be able to collect the coins
    {
        //if the player collides with a coin the coind will destroy and the scoreboard on the console will update
        if (collision.gameObject.CompareTag("Player"))
        {
            spawner.GetComponent<ScoreUpdate>().collected += 1;
            Object.Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
