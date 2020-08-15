using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingMenuUI;
    [SerializeField] private GameObject levelCompleteMenuUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Text movesText;
    [SerializeField] private Text minMovesText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text bestMovesText;
    [SerializeField] private TextMeshProUGUI levelCompleteLevelText;
    [SerializeField] private Text levelCompleteTimeText;
    [SerializeField] private Text levelCompleteMovesText;

    private void Awake()
    {
        pauseMenuUI.SetActive(false);
        settingMenuUI.SetActive(false);
        levelCompleteMenuUI.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonClick();
        }
        movesText.text = GetMovesString();
    }

    #region Helper Functions
    public void GamePause()
    {
        if (!GameManager.gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        GameManager.gameIsPaused = !GameManager.gameIsPaused;
    }
    public void InitializeUIText()
    {
        //minimal moves to complete this level
        minMovesText.text = GetMinMovesString();
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
    public void PauseButtonClick()
    {
        //can only be pressed when level isnt completed
        if (!gameManager.levelComplete)
        {
            if (!GameManager.gameIsPaused)
            {
                pauseMenuUI.SetActive(true);
            }
            else
            {
                pauseMenuUI.SetActive(false);
            }
            GamePause();
        }
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
    public void QuitGameButtonClick()
    {
        GameManager.gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void DisplayLevelCompleteUI()
    {
        GamePause();
        levelCompleteLevelText.text = GetLevelString();
        levelCompleteTimeText.text = GetCurrentLevelTimeString();
        levelCompleteMovesText.text = GetMovesString();
        levelCompleteMenuUI.SetActive(true);
    }
    public void HideLevelCompleteUI()
    {
        GamePause();
        gameManager.goToNextLevel = true;
        levelCompleteMenuUI.SetActive(false);
    }
    #endregion

    #region UI Text
    private string GetMinMovesString()
    {
        return "Min Moves: " + gameManager.GetMinimalNumberOfMovesToSolve(gameManager.numberOfDisks).ToString();
    }
    private string GetMovesString()
    {
        return "Moves: " + gameManager.numberOfMoves.ToString();
    }
    private string GetLevelString()
    {
        return "Level " + gameManager.level.ToString();
    }
    private string GetBestMovesString()
    {
        if (gameManager.bestMoves == -1)
        {
            return "Best Moves: N/A";
        }
        return "Best Moves: " + gameManager.bestMoves;
    }
    private string GetBestTimeString()
    {
        if (gameManager.bestTime == TimeSpan.Zero)
        {
            return "Time: N/A";
        }
        return "Time: " + gameManager.bestTime.ToString("mm':'ss'.'ff");
    }
    private string GetCurrentLevelTimeString()
    {
        return "Time: " + gameManager.currentLevelTime.ToString("mm':'ss'.'ff");
    }
    #endregion
}