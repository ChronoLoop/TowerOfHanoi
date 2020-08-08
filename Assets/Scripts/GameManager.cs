using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DiskSpawner diskSpawner;
    [SerializeField] private RodController firstRod;
    [SerializeField] private RodController middleRod;
    [SerializeField] private RodController lastRod;
    [SerializeField] private TimeController timeController;
    [SerializeField] private Text movesText;
    [SerializeField] private Text minMovesText;

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
    private void Start()
    {
        minMovesText.text = GetMinMovesString();
        timeController.BeginTimer();
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
            minMovesText.text = GetMinMovesString();
            diskSpawner.DestroyDisks();
            diskSpawner.InitializeDiskStack(numberOfDisks);
            timeController.ResetTimer();
        }
        movesText.text = GetMovesString();
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
    }

    private int GetMinimalNumberOfMovesToSolve(int diskCount)
    {
        return (int)Math.Pow(2, diskCount) - 1;
    }

    private string GetMinMovesString()
    {
        return "Min Moves: " + GetMinimalNumberOfMovesToSolve(numberOfDisks).ToString();
    }
    private string GetMovesString()
    {
        return "Moves: " + numberOfMoves.ToString();
    }
}