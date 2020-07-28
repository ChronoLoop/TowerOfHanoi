using UnityEngine;

public class GameManager : MonoBehaviour
{
    int numberOfMoves;
    int round;

    private void Awake()
    {
        numberOfMoves = 0;
        round = 0;
    }
}