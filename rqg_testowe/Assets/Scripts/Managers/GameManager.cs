using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private PoolManagerData poolManagerData;
    [SerializeField] private SpawnerManagerData spawnManagerData;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private Animator gameStateMachine;

    private int moveCounter;
    private float direction = -1f;
    private Coroutine movingEnemiesCoroutine;
    private Coroutine spawningEnemies;
    private bool startSpawning;

    private void Awake()
    {
        poolManagerData.OnPoolsDone += ChangeGameState;
        spawnManagerData.OnEnemiesSpawned += StartFighting;
        playerData.OnPlayerDead += GameOver;
        gameManagerData.currentEnemyQuantity = 0;
    }

    private void Update()
    {
        var currentGamePlayState = gameStateMachine.GetCurrentAnimatorStateInfo(0);
        if (currentGamePlayState.IsName("SpawningWaves"))
        {
            playerData.enableMoving = false;
            playerData.enableShooting = false;

            gameManagerData.OnSpawningWave?.Invoke();

            if (spawningEnemies == null)
            {
                spawningEnemies = StartCoroutine(spawnManager.SpawnWave());
                moveCounter = 0;
                direction = -1;
                startSpawning = false;
            }
            if (movingEnemiesCoroutine != null)
            {
                StopCoroutine(movingEnemiesCoroutine);
                movingEnemiesCoroutine = null;
            }
        }
        else if (currentGamePlayState.IsName("FightingWaves"))
        {
            playerData.enableMoving = true;
            playerData.enableShooting = true;
            spawningEnemies = null;

            if (movingEnemiesCoroutine == null)
            {
                movingEnemiesCoroutine = StartCoroutine(ManageEnemiesMoving());
            }

            if (gameManagerData.currentEnemyQuantity <= 0)
            {
                gameManagerData.currentEnemyQuantity = 0;
                if (!startSpawning)
                {
                    StartSpawning();
                    startSpawning = true;
                }
            }
        }
        else if (currentGamePlayState.IsName("GameOver"))
        {
            StopCoroutine(movingEnemiesCoroutine);
            spawnManagerData.allSpawnedEnemyObjects.ForEach(x => x.HandleDestroy());
        }
    }

    public IEnumerator ManageEnemiesMoving()
    {
        List<EnemyBase> allSpawnedEnemies = new List<EnemyBase>(spawnManagerData.allSpawnedEnemyObjects);

        while (!allSpawnedEnemies.TrueForAll(x => x.gameObject.activeSelf == false))
        {
            if (moveCounter == gameManagerData.amountOfMovesToChangeDirection)
            {
                direction *= -1f;
                moveCounter = 0;
                foreach (var enemy in allSpawnedEnemies)
                {
                    if (!enemy.gameObject.activeSelf)
                    {
                        continue;
                    }

                    enemy.Move(0f, -Vector3.forward);
                    yield return new WaitForSeconds(0.1f);
                }
            }
            else
            {
                foreach (var enemy in allSpawnedEnemies)
                {
                    if (!enemy.gameObject.activeSelf)
                    {
                        continue;
                    }

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

    public void ChangeGameplayState(Animator stateMachine, string parameter)
    {
        stateMachine.SetTrigger(parameter);
    }


    public void StartSpawning()
    {
        ChangeGameplayState(gameStateMachine, "SpawningWaves");
    }
    public void StartFighting()
    {
        ChangeGameplayState(gameStateMachine, "FightingWaves");
    }

    public void GameOver()
    {
        ChangeGameplayState(gameStateMachine, "GameOver");
        ChangeGameState(3);
    }

}
