using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "SpawnerManagerData" , menuName = "Managers/SpawnerManagerData")]
public class SpawnerManagerData : ScriptableObject
{
    public float xSpacing;
    public float ySpacing;

    public Action OnEnemiesSpawned;

    [HideInInspector]
    public List<EnemyBase> allSpawnedEnemyObjects = new List<EnemyBase>();
}
