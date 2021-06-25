using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat
{
    private PoolManagerData poolManagerData;
    private Transform barrelTransform;
    private EnemyData enemyData;

    private float shootingTimer;

    public EnemyCombat(Transform barrelTransform, EnemyData enemyData, PoolManagerData poolManagerData)
    {
        this.barrelTransform = barrelTransform;
        this.enemyData = enemyData;
        this.poolManagerData = poolManagerData;
        this.shootingTimer = Random.Range(-1f, -5f);
    }

    public void HandleShooting()
    {
        shootingTimer += Time.deltaTime;
        if(AbleToShoots() && shootingTimer >= enemyData.shootDelay)
        {
            Shoot();
            shootingTimer = 0f;
        }
    }

    private bool AbleToShoots()
    {
        RaycastHit hit;

        Physics.Raycast(barrelTransform.position, -Vector3.forward, out hit);

        return hit.collider == null || hit.collider.tag == "Player" ;
    }

    private void Shoot()
    {
        if (poolManagerData.allPools.ContainsKey("EnemyProjectile") == false)
        {
            return;
        }

        GameObject projectile = poolManagerData.allPools["EnemyProjectile"].Dequeue();
        var iPooledObject = projectile.GetComponent<IPooledObject>();
        iPooledObject.MyPool = poolManagerData.allPools["EnemyProjectile"];
        projectile.transform.position = new Vector3(barrelTransform.position.x, barrelTransform.position.y, barrelTransform.position.z);
        projectile.transform.rotation = barrelTransform.rotation;
        projectile.SetActive(true);       
    }
}
