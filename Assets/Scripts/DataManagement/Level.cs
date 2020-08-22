using System;

[System.Serializable]
public class Level
{
    public int level { get; private set; }
    public float bestTime { get; private set; }
    public int bestMoves { get; private set; }
    public Level(GameManager gameManager)
    {
        level = gameManager.level;
        bestTime = gameManager.currentLevelTime;
        bestMoves = gameManager.numberOfMoves;
    }
    public void SetBestScores(GameManager gameManager)
    {
        if (gameManager.currentLevelTime < bestTime)
        {
            bestTime = gameManager.currentLevelTime;
        }
        if (gameManager.numberOfMoves < bestMoves)
        {
            bestMoves = gameManager.numberOfMoves;
        }
    }
}
