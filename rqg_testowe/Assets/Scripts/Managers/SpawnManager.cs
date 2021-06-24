using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private PoolManagerData poolManagerData;
    [SerializeField] private GameManagerData gameManagerData;
    [SerializeField] private SpawnerManagerData spawnerManagerData;
    [SerializeField] private Transform spawnParent;

    private List<Vector3> grid;

    [Tooltip("First element in list is first row above player")]
    [SerializeField] private List<SpawnTemplate> spawnTemplate = new List<SpawnTemplate>();


    private void Awake()
    {
        grid = GenerateGrid();
        gameManagerData.AfterGameStateChange += CreateWave;
    }

    public void CreateWave(int gameState)
    {
        if ((GameState)gameState == GameState.Gameplay)
        {
            StartCoroutine(SpawnWave());
        }
    }

    public IEnumerator SpawnWave()
    {
        int gridIndex = 0;
        spawnerManagerData.allSpawnedEnemyObjects.Clear();

        foreach (var element in spawnTemplate)
        {
            for (int i = 0; i < element.quantity; i++)
            {               
                GameObject enemyObject = poolManagerData.allPools[element.poolName].Dequeue();
                var iPooledObject = enemyObject.GetComponent<IPooledObject>();
                iPooledObject.MyPool = poolManagerData.allPools[element.poolName];
                enemyObject.SetActive(true);
                enemyObject.transform.position = grid[gridIndex];
                enemyObject.transform.rotation = Quaternion.Euler(0f, 180f, 0);
                gridIndex++;
                
                spawnerManagerData.allSpawnedEnemyObjects.Add(enemyObject.GetComponent<EnemyBase>());
                //just for visual effect
                yield return new WaitForSeconds(0.05f);
            }
        }

        spawnerManagerData.OnEnemiesSpawned?.Invoke();
    }

    private List<Vector3> GenerateGrid()
    {
        List<Vector3> grid = new List<Vector3>();

        //by design
        int columns = 10;
        int rows = 5;

        for (int i = 0; i < columns * rows; i++)
        {
            Vector3 gridElement = new Vector3(spawnParent.position.x + (
                spawnerManagerData.xSpacing * (i % columns)),
                0f,
                spawnParent.position.z + (spawnerManagerData.ySpacing * (i / columns)));
            grid.Add(gridElement);
        }

        return grid;
    }

    [Serializable]
    public struct SpawnTemplate
    {
        public string poolName;
        public int quantity;
    }
}
