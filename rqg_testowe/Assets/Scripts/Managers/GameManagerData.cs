using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Managers/GameManagerData")]
public class GameManagerData : ScriptableObject
{   
    public GameState currentGameState;

    public int currentEnemyQuantity;
    public int amountOfMovesToChangeDirection;
    public int timeToNextMove;
    public int score;
    public int currentWave;
    public int wavePassed;

    public Action OnGameStateChange;
    public Action<int> AfterGameStateChange;

    public Action OnSpawningWave;
    public Action OnFightingEnemies;
    public Action OnGameOver;
    
    public void IncreaseScore(int amount)
    {
        score += amount;
        currentEnemyQuantity--;
    }
}

public enum GameState
{
    Loading = 0,
    MainMenu = 1,
    Gameplay = 2,
    Results = 3,
    Highscores = 4,
}

