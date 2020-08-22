using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingMenuUI;
    [SerializeField] private GameObject levelCompleteMenuUI;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TimeController gameManagerTimeController;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private SceneController sceneController;
    [SerializeField] private TextMeshProUGUI movesText;
    [SerializeField] private TextMeshProUGUI minMovesText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI bestTimeText;
    [SerializeField] private TextMeshProUGUI bestMovesText;
    [SerializeField] private TextMeshProUGUI levelCompleteLevelText;
    [SerializeField] private TextMeshProUGUI levelCompleteTimeText;
    [SerializeField] private TextMeshProUGUI levelCompleteMovesText;
    [SerializeField] private TextMeshProUGUI timeCounterText;

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
        if (Input.GetKeyDown(KeyCode.F))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
        movesText.text = GetMovesString();
        timeCounterText.text = GetTimePlayingString();
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
        soundManager.SaveSoundSetting();
    }
    public void QuitGameButtonClick()
    {
        GameManager.gameIsPaused = false;
        Time.timeScale = 1f;
        sceneController.LoadMainMenuScene();
    }
    public void RestartButtonClick()
    {
        gameManager.restartLevel = true;
        //unpause game if restart
        if (GameManager.gameIsPaused && gameManager.levelComplete)
        {
            GamePause();
            levelCompleteMenuUI.SetActive(false);
        }
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
        if (gameManager.bestTime == 0f)
        {
            return "Best Time: N/A";
        }
        TimeSpan time = TimeSpan.FromSeconds(gameManager.bestTime);
        return "Best Time: " + time.ToString("mm':'ss'.'ff");
    }
    private string GetCurrentLevelTimeString()
    {
        TimeSpan time = TimeSpan.FromSeconds(gameManager.currentLevelTime);
        return "Time: " + time.ToString("mm':'ss'.'ff");
    }
    public string GetTimePlayingString()
    {
        TimeSpan timePlaying = TimeSpan.FromSeconds(gameManagerTimeController.GetTimePlaying());
        return "Time: " + timePlaying.ToString("mm':'ss'.'ff");
    }
    #endregion
}