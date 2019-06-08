using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreenManager : MonoBehaviour
{
    public void handleYesButton()
    {
        GameManager.Instance.restartGame();
    }

    public void handleNoButton()
    {
        GameManager.Instance.loadMenu();
    }
}
