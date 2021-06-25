using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBase : MonoBehaviour, IPooledObject, IDestructable
{
    [SerializeField] private PoolManagerData poolManagerData;
    [SerializeField] private EnemyData enemyData;    
    [SerializeField] private Transform barrelTransform;
    [HideInInspector] public EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;
    private bool enableShooting;

    public Action<int> OnEnemyDestroy;

    public Queue<GameObject> MyPool { get; set; }

    private void Awake()
    {
        enemyMovement = new EnemyMovement(transform, Vector3.left);
        enemyCombat = new EnemyCombat(barrelTransform, enemyData, poolManagerData);
    }

    void Update()
    {
        if(enableShooting == false)
        {
            return;
        }

        enemyCombat.HandleShooting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag(other.GetComponent<AmmunitionBase>().GetAffectedTag()))
        {
            OnEnemyDestroy?.Invoke(enemyData.pointsGranted);
            HandleDestroy();
        }
    }

    public void Move(float direction, Vector3 offset)
    {
        transform.position = new Vector3(transform.position.x + (direction * offset.x), transform.position.y , transform.position.z + offset.z);
    }

    public void ReturnToPool()
    {
        MyPool.Enqueue(this.gameObject);
    }

    public void EnableShooting()
    {
        enableShooting = true;
    }

    public void HandleDestroy()
    {
        gameObject.SetActive(false);
        ReturnToPool();
    }
}
