using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject[] coins;
    public int totalCoins = 15; //coins that I want in total
    public int coinsPerRow = 5; //coins per row
    public float spacing = 1.5f; //spacing between coins
    // Start is called before the first frame update
    void Start()
    {

        coins = new GameObject[totalCoins];

        for (int i = 0; i < coins.Length; i++)
        {
            //coins[i] = coinPrefab;

            //Debug.Log("Array Slot" + i + ": " + coins[i].name);

            //row and column for grid
            int row = i / coinsPerRow;
            int column = i % coinsPerRow;//% = divide

            //removed "1" after spacing and replaced with 7.2f because coins would spawn below the plane
            Vector3 position = new Vector3(spacing * row, 7.2f, spacing * column);

            //instantiate the coins based on the calculated position
            coins[i] = Instantiate(coinPrefab, position, Quaternion.identity, this.transform) as GameObject;

        }
    }

    public GameObject[] GetCoins()
    {
        return coins;
    }
}