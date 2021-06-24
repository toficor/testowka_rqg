using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private PoolManagerData poolManagerData;


    private void Awake()
    {
        poolManagerData.OnPoolsDone += ChangeGameState;
    }

    public void ChangeGameState(int gameState)
    {        
        if (gameManagerData.currentGameState != (GameState)gameState)
        {
            gameManagerData.OnGameStateChange?.Invoke();
        }

        gameManagerData.currentGameState = (GameState)gameState;

        gameManagerData.AfterGameStateChange?.Invoke();
    }
}
