using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform barrelTransform;
    [HideInInspector] public EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;

    private void Awake()
    {
        enemyMovement = new EnemyMovement(transform, Vector3.left);
        enemyCombat = new EnemyCombat(barrelTransform, enemyData);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCombat.HandleShooting();
    }
}
