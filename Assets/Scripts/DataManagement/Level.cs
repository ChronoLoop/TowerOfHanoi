using System;
using System.Collections;

[System.Serializable]
public class Level
{
    public int level { get; private set; }
    public TimeSpan bestTime { get; private set; }
    public int bestMoves { get; private set; }
    public Level(GameManager gameManager)
    {
        level = gameManager.level;
        bestTime = gameManager.bestTime;
        bestMoves = gameManager.numberOfMoves;
    }
    public void SetBestScores(GameManager gameManager)
    {
        if (gameManager.bestTime < bestTime)
        {
            bestTime = gameManager.bestTime;
        }
        if (gameManager.numberOfMoves < bestMoves)
        {
            bestMoves = gameManager.numberOfMoves;
        }
    }
}
