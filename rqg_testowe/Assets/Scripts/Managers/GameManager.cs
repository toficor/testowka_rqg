using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private PoolManagerData poolManagerData;
    [SerializeField] private SpawnerManagerData spawnManagerData;
    [SerializeField] private SpawnManager spawnManager;

    private int moveCounter;
    private float direction = -1f;

    private void Awake()
    {
        poolManagerData.OnPoolsDone += ChangeGameState;
        spawnManagerData.OnEnemiesSpawned += StartMovingEnemies;
    }

    private void StartMovingEnemies()
    {
        StartCoroutine(ManageEnemiesMoving());
    }

    public IEnumerator ManageEnemiesMoving()
    {
        while (true)
        {
            if (moveCounter == gameManagerData.amountOfMovesToChangeDirection)
            {
                direction *= -1f;
                moveCounter = 0;
                foreach (var enemy in spawnManagerData.allSpawnedEnemyObjects)
                {
                    enemy.Move(0f, -Vector3.forward);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                foreach (var enemy in spawnManagerData.allSpawnedEnemyObjects)
                {
                    enemy.Move(direction, Vector3.right);
                    yield return new WaitForSeconds(0.1f);
                }
                moveCounter++;
            }

            
        }
    }



    public void ChangeGameState(int gameState)
    {
        if (gameManagerData.currentGameState != (GameState)gameState)
        {
            gameManagerData.OnGameStateChange?.Invoke();
        }

        gameManagerData.currentGameState = (GameState)gameState;

        gameManagerData.AfterGameStateChange?.Invoke(gameState);
    }
}
