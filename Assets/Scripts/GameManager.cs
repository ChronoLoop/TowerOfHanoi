using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DiskSpawner diskSpawner;
    [SerializeField] private RodController firstRod;
    [SerializeField] private RodController middleRod;
    [SerializeField] private RodController lastRod;
    [SerializeField] private TimeController timeController;
    [SerializeField] private Text movesText;
    [SerializeField] private Text minMovesText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text bestMovesText;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingMenuUI;
    public static bool gameIsPaused;
    public List<Level> levelsList { get; private set; }
    public int numberOfMoves { get; private set; }
    public int level { get; private set; }
    public int numberOfDisks { get; private set; }
    public bool restartLevel { get; private set; }
    public TimeSpan bestTime { get; private set; }
    public int bestMoves { get; private set; }

    private void Awake()
    {
        level = 1;
        numberOfMoves = 0;
        numberOfDisks = 3;
        bestMoves = -1;
        restartLevel = false;
        gameIsPaused = false;
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        diskSpawner.InitializeDiskStack(numberOfDisks);
        SetUpRodEvents();
        LoadLevelData();
        SetCurrentLevelBestScores();
        InitializeUIText();
    }
    private void Start()
    {
        timeController.BeginTimer();
    }
    private void Update()
    {
        if (CheckWinCondition())
        {
            AddCurrentLevel();
            level++;
            numberOfDisks++;
            ResetBoard();
        }
        if (restartLevel)
        {
            ResetBoard();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                ExitPauseButtonClick();
            }
            else
            {
                PauseButtonClick();
            }
        }
        movesText.text = GetMovesString();
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

    private int GetMinimalNumberOfMovesToSolve(int diskCount)
    {
        return (int)Math.Pow(2, diskCount) - 1;
    }
    private void ResetBoard()
    {
        numberOfMoves = 0;
        restartLevel = false;
        firstRod.ClearStack();
        middleRod.ClearStack();
        lastRod.ClearStack();
        minMovesText.text = GetMinMovesString();
        SetCurrentLevelBestScores();
        InitializeUIText();
        diskSpawner.DestroyDisks();
        diskSpawner.InitializeDiskStack(numberOfDisks);
        timeController.ResetTimer();
    }

    #region UI Text
    private string GetMinMovesString()
    {
        return "Min Moves: " + GetMinimalNumberOfMovesToSolve(numberOfDisks).ToString();
    }
    private string GetMovesString()
    {
        return "Moves: " + numberOfMoves.ToString();
    }
    private string GetLevelString()
    {
        return "Level " + level.ToString();
    }
    private string GetBestMovesString()
    {
        if (bestMoves == -1)
        {
            return "Best Moves: N/A";
        }
        return "Best Moves: " + bestMoves;
    }
    private string GetBestTimeString()
    {
        if (bestTime == TimeSpan.Zero)
        {
            return "Time: N/A";
        }
        return "Time: " + bestTime.ToString("mm':'ss'.'ff");
    }
    private void InitializeUIText()
    {
        //current level record scores
        bestTimeText.text = GetBestTimeString();
        bestMovesText.text = GetBestMovesString();
        //current level data
        movesText.text = GetMovesString();
        minMovesText.text = GetMinMovesString();
        levelText.text = GetLevelString();
    }
    #endregion

    #region Icon Buttons on click functions
    public void RestartButtonClick()
    {
        restartLevel = true;
    }
    public void PauseButtonClick()
    {
        pauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }
    public void ExitPauseButtonClick()
    {
        pauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }
    public void SettingButtonClick()
    {
        settingMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
    public void ExitSettingButtonClick()
    {
        settingMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
    public void QuitButtonClick()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    #endregion

    #region level data management
    private void AddCurrentLevel()
    {
        bestTime = timeController.GetTimePlaying();
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
            bestTime = TimeSpan.Zero;
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
    #endregion

}