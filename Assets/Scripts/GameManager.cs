using UnityEngine;

public class GameManager : MonoBehaviour
{
    public DiskSpawner diskSpawner;
    public RodController middlePole;
    public RodController lastPole;
    private int numberOfMoves;
    private int numberOfDisks;
    private int level;


    private void Awake()
    {
        level = 1;
        numberOfMoves = 0;
        numberOfDisks = 3;
        diskSpawner.InitializeDiskStack(numberOfDisks);
    }
    private void SpawnDisks()
    {
        diskSpawner.InitializeDiskStack(numberOfDisks);
    }
    private void Update()
    {
        if (checkWinCondition())
        {
            level++;
            numberOfDisks++;
            diskSpawner.DestroyDisks();
            diskSpawner.InitializeDiskStack(numberOfDisks);
        }
    }

    bool checkWinCondition()
    {
        //if all disks are stack on either the middle or last pole
        if (middlePole.getDiskCount() == numberOfDisks || lastPole.getDiskCount() == numberOfDisks)
        {
            return true;
        }
        return false;
    }
}