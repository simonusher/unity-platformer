using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void handleGameStart()
    {
        GameManager.Instance.loadNextLevel();
    }

    public void handleGameExit()
    {
        GameManager.Instance.exitGame();
    }
}
