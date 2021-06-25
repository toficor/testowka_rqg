using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IPooledObject, IDestructable
{
    [SerializeField] private PoolManagerData poolManagerData;
    [SerializeField] private EnemyData enemyData;
    [SerializeField] private Transform barrelTransform;
    [HideInInspector] public EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;

    public Queue<GameObject> MyPool { get; set; }

    private void Awake()
    {
        enemyMovement = new EnemyMovement(transform, Vector3.left);
        enemyCombat = new EnemyCombat(barrelTransform, enemyData, poolManagerData);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCombat.HandleShooting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
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

    public void HandleDestroy()
    {
        gameObject.SetActive(false);
        ReturnToPool();
    }
}
