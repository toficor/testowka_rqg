using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Managers/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    public Animator gameStateMachine;
    public GameState currentGameState;

    public int currentEnemyQuantity;
    public int amountOfMovesToChangeDirection;
    public int timeToNextMove;

    public Action OnGameStateChange;
    public Action<int> AfterGameStateChange;   
    
}

public enum GameState
{
    Loading = 0,
    MainMenu = 1,
    Gameplay = 2,
    Results = 3,
    Highscores = 4,
}

