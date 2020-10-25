using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //import exteral method from plugin/webgl/HandleWin.jslib
    [DllImport("__Internal")]
    private static extern void SendScores(int level, int moves, float time);
    [SerializeField] private DiskSpawner diskSpawner;
    [SerializeField] private RodController firstRod;
    [SerializeField] private RodController middleRod;
    [SerializeField] private RodController lastRod;
    [SerializeField] private TimeController timeController;
    [SerializeField] private GameUIManager gameUIManager;
    [SerializeField] private SceneController sceneController;
    public static bool gameIsPaused;
    public List<Level> levelsList { get; private set; }
    public int numberOfMoves { get; private set; }
    public int level { get; private set; }
    public int numberOfDisks { get; private set; }
    public bool restartLevel { get; set; }
    public bool levelComplete { get; set; }
    public bool goToNextLevel { get; set; }
    public float currentLevelTime { get; private set; }
    public float bestTime { get; private set; }
    public int bestMoves { get; private set; }
    public static bool gameHasStarted;

    private void Awake()
    {
        InitializeBoard();
    }
    private void Update()
    {
        if (firstRodHasAllDisks())
        {
            if (!gameHasStarted)
            {
                BeginLevel();
            }
            gameHasStarted = true;
        }
        if (numberOfDisks > 10)
        {
            ExitGame();
        }
        if (CheckWinCondition() && !levelComplete)
        {
            EndLevel();
        }
        if (restartLevel)
        {
            timeController.EndTimer();
            ResetBoard();
        }
        if (goToNextLevel)
        {
            level++;
            numberOfDisks++;
            ResetBoard();
        }
    }

    #region helper functions
    private void InitializeBoard()
    {
        level = 1;
        numberOfMoves = 0;
        numberOfDisks = 3;
        bestMoves = -1;
        restartLevel = false;
        gameIsPaused = false;
        goToNextLevel = false;
        levelComplete = false;
        gameHasStarted = false;
        diskSpawner.InitializeDiskStack(numberOfDisks);
        SetUpRodEvents();
        LoadLevelData();
        SetCurrentLevelBestScores();
        gameUIManager.InitializeUIText();
    }
    private void ResetBoard()
    {
        timeController.ResetTimer();
        numberOfMoves = 0;
        restartLevel = false;
        goToNextLevel = false;
        levelComplete = false;
        gameHasStarted = false;
        ClearRodStacks();
        SetCurrentLevelBestScores();
        gameUIManager.InitializeUIText();
        diskSpawner.DestroyDisks();
        diskSpawner.InitializeDiskStack(numberOfDisks);
    }
    private void ClearRodStacks()
    {
        firstRod.ClearStack();
        middleRod.ClearStack();
        lastRod.ClearStack();
    }
    public int GetMinimalNumberOfMovesToSolve(int diskCount)
    {
        return (int)Math.Pow(2, diskCount) - 1;
    }
    private void SetUpRodEvents()
    {
        firstRod.DiskDropSuccessfulEvent += IncrementMove;
        middleRod.DiskDropSuccessfulEvent += IncrementMove;
        lastRod.DiskDropSuccessfulEvent += IncrementMove;
    }
    private void IncrementMove(object src, EventArgs e)
    {
        numberOfMoves++;
    }
    private bool CheckWinCondition()
    {
        //check if all disks are not moving and stacked on one rod
        bool middleRodWinCondition = (middleRod.GetDiskCount() == numberOfDisks) && !middleRod.AreDisksMoving();
        bool lastRodWinCondition = (lastRod.GetDiskCount() == numberOfDisks) && !lastRod.AreDisksMoving();

        if (middleRodWinCondition || lastRodWinCondition)
        {
            return true;
        }

        return false;
    }
    private static void WebGLSendScores(int level, int moves, float time)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            SendScores(level, moves, time);
        }
    }
    #endregion

    #region Icon Buttons on click functions

    #endregion

    #region level data management
    private void AddCurrentLevel()
    {
        currentLevelTime = timeController.GetTimePlaying();
        Level currentLevel = new Level(this);
        /*
        check if currentLevel exists in list
        if it exists, replace values
        */
        if (LevelListIndexExists(currentLevel.level))
        {
            int index = currentLevel.level - 1;
            //replace best scores for the current level in the levelsList if the scores are better 
            levelsList[index].SetBestScores(this);
        }
        else
        {
            levelsList.Add(currentLevel);
        }
        SaveLevelData();
    }
    private void LoadLevelData()
    {
        LevelData data = DataManager.LoadLevelData();
        if (data == null)
        {
            //if there is no level data, then use a new list of levels
            levelsList = new List<Level>();
        }
        else
        {
            levelsList = data.levelsList;
        }
    }
    private void SaveLevelData()
    {
        DataManager.SaveLevelData(this);
    }

    private void SetCurrentLevelBestScores()
    {
        if (LevelListIndexExists(level))
        {
            bestMoves = levelsList[level - 1].bestMoves;
            bestTime = levelsList[level - 1].bestTime;
        }
        else
        {
            bestTime = 0f;
            bestMoves = -1;
        }
    }
    private bool LevelListIndexExists(int level)
    {
        int index = level - 1;
        if (index < levelsList.Count)
        {
            return true;
        }
        return false;
    }
    private void BeginLevel()
    {
        timeController.BeginTimer();
    }
    private void EndLevel()
    {
        timeController.EndTimer();
        AddCurrentLevel();
        WebGLSendScores(level, numberOfMoves, currentLevelTime);
        levelComplete = true;
        gameUIManager.DisplayLevelCompleteUI();
    }

    private void ExitGame()
    {
        sceneController.LoadCreditsScene();
    }

    private bool firstRodHasAllDisks()
    {
        return firstRod.GetDiskCount() == numberOfDisks ? true : false;
    }
    #endregion


}