using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedSceneManager : MonoBehaviour
{
    public void mainMenuButtonClicked()
    {
        GameManager.Instance.loadMenu();
    }

    public void exitButtonClicked()
    {
        GameManager.Instance.exitGame();
    }
}
