using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Score scoreBoard;
    public TreasureGenerator treasureGenerator;

    public static GameManager instance = null;
    void Start()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        StartGame();
    }

    public void StartGame()
    {
        scoreBoard.ResetScore();
        treasureGenerator.StartTreasureGenerator();
    }

    public void AddPoints(int pointsToAdd)
    {
        scoreBoard.AddToScore(pointsToAdd);
    }
}
