using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string menuSceneName = "Menu";
    [SerializeField] private string deathSceneName = "Death";
    [SerializeField] private string finishSceneName = "Finish";

    [SerializeField] private string[] levelNames;

    [SerializeField] private float timeBetweenScenes;

    private int currentLevelNumber = -1;
    private bool levelLoading;

    public static GameManager Instance = null;

    private void Start()
    {
        levelLoading = false;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(Instance != this)
        {
            Debug.Log("Destroy manager");
            Destroy(gameObject);
        }
    }

    public void loadMenu()
    {
        currentLevelNumber = -1;
        SceneManager.LoadScene(menuSceneName);
    }

    public void loadNextLevel()
    {
        if (!levelLoading) {
            currentLevelNumber++;
            if (currentLevelNumber >= levelNames.Length)
            {
                handleGameFinished();
            }
            else
            {
                StartCoroutine("loadCurrentLevel");
            }
        }
    }

    public void handlePlayerDeath()
    {
        loadDeathScreen();
    }

    public void restartGame()
    {
        currentLevelNumber = -1;
        loadNextLevel();
    }

    public void exitGame()
    {
        Application.Quit();
    }

    private IEnumerator loadCurrentLevel()
    {
        levelLoading = true;
        if(currentLevelNumber != 0)
        {
            yield return new WaitForSeconds(timeBetweenScenes);
        }
        SceneManager.LoadScene(levelNames[currentLevelNumber]);
        levelLoading = false;
    }

    private void handleGameFinished()
    {
        loadFinishedScene();
    }

    private void loadFinishedScene()
    {
        SceneManager.LoadScene(finishSceneName);
    }

    private void loadDeathScreen()
    {
        SceneManager.LoadScene(deathSceneName);
    }
}
