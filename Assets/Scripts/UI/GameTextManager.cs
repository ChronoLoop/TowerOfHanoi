using System;
using UnityEngine;
using UnityEngine.UI;

public class GameTextManager : MonoBehaviour
{
    [SerializeField] private Text movesText;
    [SerializeField] private Text minMovesText;
    [SerializeField] private Text levelText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Text bestMovesText;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        movesText.text = GetMovesString();
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
    #endregion
}