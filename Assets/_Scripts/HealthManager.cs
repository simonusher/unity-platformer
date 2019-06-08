using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenHits;
    private int healthsLeft;
    private int startingHeartsNumber = 3;
    private bool canGetHit;
    void Start()
    {
        healthsLeft = startingHeartsNumber;
        canGetHit = true;
    }

    public int getHealthsLeft()
    {
        return healthsLeft;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            handleEnemyCollision();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            handleEnemyCollision();
        }

    }

    private void handleEnemyCollision()
    {
        if (canGetHit)
        {
            healthsLeft--;
            if (healthsLeft <= 0)
            {
                handleDeath();
            }
            StartCoroutine("toggleCanGetHit");
        }
    }

    private void handleDeath()
    {
        GameManager.Instance.handlePlayerDeath();
    }

    IEnumerator toggleCanGetHit()
    {
        canGetHit = false;
        yield return new WaitForSeconds(timeBetweenHits);
        canGetHit = true;
    }

    public bool possibleToHit()
    {
        return canGetHit;
    }
}
