using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{
    [SerializeField] HealthManager healthManager;
    [SerializeField] PickupManager pickupManager;
    [SerializeField] private string scoreTemplate = " Coins";
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject[] hearts;
    private int activeHeartsLeft;
    void Start()
    {
        updateScoreText();
        activeHeartsLeft = hearts.Length;
    }

    void Update()
    {
        updateScoreText();
        updateHearts();
    }

    private void updateScoreText()
    {
        int coinsToCollect = pickupManager.getNumberOfCoinsToCollect();
        int coinsCollected = pickupManager.getNumberOfCollectedCoins();
        scoreText.text = coinsCollected + "/" + coinsToCollect + scoreTemplate;
    }

    private void updateHearts()
    {
        int healthsLeft = healthManager.getHealthsLeft();
        if(activeHeartsLeft > healthsLeft)
        {
            activeHeartsLeft--;
            hearts[activeHeartsLeft].SetActive(false);
        }
    }
}
