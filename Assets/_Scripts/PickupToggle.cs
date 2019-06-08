using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupToggle : MonoBehaviour
{
    [SerializeField] PickupManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Coin")
        {
            manager.handleCoinPickup(collision.gameObject);
        }
    }
}
