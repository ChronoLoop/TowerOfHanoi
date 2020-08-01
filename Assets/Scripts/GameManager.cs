using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DiskSpawner diskSpawner;
    [SerializeField]
    private RodController middleRod;
    [SerializeField]
    private RodController lastRod;
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
        //check if all disks are not moving and stacked on one rod
        bool middleRodWinCondition = middleRod.GetDiskCount() == numberOfDisks && !middleRod.AreDisksMoving();
        bool lastRodWinCondition = lastRod.GetDiskCount() == numberOfDisks && !lastRod.AreDisksMoving();
        if (middleRodWinCondition || lastRodWinCondition)
        {
            middleRod.ClearStack();
            lastRod.ClearStack();
            return true;
        }
        return false;
    }
}