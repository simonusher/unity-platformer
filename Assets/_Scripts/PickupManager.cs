using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private GameObject[] coinsToCollect;
    private int collectedCoins;
    private void Start()
    {
        coinsToCollect = GameObject.FindGameObjectsWithTag("Coin");
        collectedCoins = 0;
    }
    public void handleCoinPickup(GameObject coin)
    {
        collectedCoins++;
        coin.SetActive(false);
    }

    public int getNumberOfCoinsToCollect()
    {
        if(coinsToCollect == null)
        {
            return 0;
        }
        return coinsToCollect.Length;
    }

    public int getNumberOfCollectedCoins()
    {
        return collectedCoins;
    }
}
