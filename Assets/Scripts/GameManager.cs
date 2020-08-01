using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private DiskSpawner diskSpawner;
    [SerializeField]
    private RodController middlePole;
    [SerializeField]
    private RodController lastPole;
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
        bool middlePoleWinCondition = middlePole.GetDiskCount() == numberOfDisks && !middlePole.AreDisksMoving();
        bool lastPoleWinCondition = lastPole.GetDiskCount() == numberOfDisks && !lastPole.AreDisksMoving();
        if (middlePoleWinCondition || lastPoleWinCondition)
        {
            middlePole.ClearStack();
            lastPole.ClearStack();
            return true;
        }
        return false;
    }
}