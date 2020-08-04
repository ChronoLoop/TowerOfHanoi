using UnityEngine;
using System;

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
        numberOfDisks = 3;
        diskSpawner.InitializeDiskStack(numberOfDisks);
        SetUpRodEvents();
    }
    private void Update()
    {
        if (CheckWinCondition())
        {
            level++;
            numberOfDisks++;
            numberOfMoves = 0;
            firstRod.ClearStack();
            middleRod.ClearStack();
            lastRod.ClearStack();
            diskSpawner.DestroyDisks();
            diskSpawner.InitializeDiskStack(numberOfDisks);
        }
    }

    private bool CheckWinCondition()
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
    private void SetUpRodEvents()
    {
        firstRod.DiskDropSuccessfulEvent += IncrementMove;
        middleRod.DiskDropSuccessfulEvent += IncrementMove;
        lastRod.DiskDropSuccessfulEvent += IncrementMove;
    }
    private void IncrementMove(object src, EventArgs e)
    {
        numberOfMoves++;
        Debug.Log(numberOfMoves);
    }

    private int GetMinimalNumberOfMovesToSolve(int diskCount)
    {
        return (int)Math.Pow(2, diskCount);
    }

}