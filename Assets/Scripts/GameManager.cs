using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DiskSpawner diskSpawner;
    [SerializeField]
    private RodController firstRod;
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
        numberOfDisks = 7;
        diskSpawner.InitializeDiskStack(numberOfDisks);
    }
    private void Update()
    {
        if (checkWinCondition())
        {
            level++;
            numberOfDisks++;
            firstRod.ClearStack();
            middleRod.ClearStack();
            lastRod.ClearStack();
            diskSpawner.DestroyDisks();
            diskSpawner.InitializeDiskStack(numberOfDisks);
        }
    }

    bool checkWinCondition()
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
}