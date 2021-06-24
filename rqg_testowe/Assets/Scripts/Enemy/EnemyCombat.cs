using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat
{
    private Transform barrelTransform;
    private EnemyData enemyData;

    private float shootingTimer;

    public EnemyCombat(Transform barrelTransform, EnemyData enemyData)
    {
        this.barrelTransform = barrelTransform;
        this.enemyData = enemyData;
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
        GameObject.Instantiate(enemyData.ammo, barrelTransform.position, barrelTransform.rotation);
    }
}
