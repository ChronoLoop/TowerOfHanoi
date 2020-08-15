using System.Collections.Generic;

[System.Serializable]
public class LevelData
{
    //the index for each level data = level number -1
    //example: level 1 will be stored at index 0 in the list
    public List<Level> levelsList;
    public LevelData(GameManager gameManager)
    {
        levelsList = gameManager.levelsList;
    }
}