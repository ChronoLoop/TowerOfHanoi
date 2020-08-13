using System;
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
    [SerializeField] private Text LevelText;
    [SerializeField] private GameObject pauseMenuGameObj;
    [SerializeField] private GameObject settingMenuGameObj;

    private int numberOfMoves;
    private int numberOfDisks;
    private int level;
    private bool restartLevel;

    private void Awake()
    {
        level = 1;
        numberOfMoves = 0;
        numberOfDisks = 3;
        restartLevel = false;
        pauseMenuGameObj.SetActive(false);
        settingMenuGameObj.SetActive(false);
        diskSpawner.InitializeDiskStack(numberOfDisks);
        SetUpRodEvents();
    }
    private void Start()
    {
        minMovesText.text = GetMinMovesString();
        LevelText.text = GetLevelString();
        timeController.BeginTimer();
    }
    private void Update()
    {
        if (CheckWinCondition())
        {
            level++;
            numberOfDisks++;
            ResetBoard();
        }
        if (restartLevel)
        {
            ResetBoard();
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
    #endregion

    private void ResetBoard()
    {
        numberOfMoves = 0;
        restartLevel = false;
        firstRod.ClearStack();
        middleRod.ClearStack();
        lastRod.ClearStack();
        minMovesText.text = GetMinMovesString();
        LevelText.text = GetLevelString();
        diskSpawner.DestroyDisks();
        diskSpawner.InitializeDiskStack(numberOfDisks);
        timeController.ResetTimer();
    }

    #region Icon Buttons on click functions
    public void RestartButtonClick()
    {
        restartLevel = true;
    }
    public void PauseButtonClick()
    {
        pauseMenuGameObj.SetActive(true);
    }
    public void ExitPauseButtonClick()
    {
        pauseMenuGameObj.SetActive(false);
    }
    public void SettingButtonClick()
    {
        settingMenuGameObj.SetActive(true);
        pauseMenuGameObj.SetActive(false);
    }
    public void ExitSettingButtonClick()
    {
        settingMenuGameObj.SetActive(false);
        pauseMenuGameObj.SetActive(true);
    }
    public void QuitButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    #endregion
}