using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat
{
    private PoolManagerData poolManagerData;
    private Transform barrelTransform;
    private PlayerData playerData;
    private PlayerInput playerInput;

    private float shootingTimmer;

    public PlayerCombat(PlayerData playerData, PlayerInput playerInput, Transform barellTransform, PoolManagerData poolManagerData)
    {
        this.barrelTransform = barellTransform;
        this.playerData = playerData;
        this.playerInput = playerInput;
        this.poolManagerData = poolManagerData;
        this.shootingTimmer = 0f;
    }

    public void HandleShooting()
    {
        shootingTimmer += Time.deltaTime;

        if (playerInput.IsShooting)
        {
            if (shootingTimmer >= playerData.shootingDelay)
            {
                Shoot();
                shootingTimmer = 0f;
            }
        }
    }

    private void Shoot()
    {
        if(poolManagerData.allPools.ContainsKey("Projectile") == false)
        {
            return;
        }

        GameObject projectile = poolManagerData.allPools["Projectile"].Dequeue();
        var iPooledObject = projectile.GetComponent<IPooledObject>();
        iPooledObject.MyPool = poolManagerData.allPools["Projectile"];
        projectile.transform.position = new Vector3(barrelTransform.position.x, barrelTransform.position.y, barrelTransform.position.z);
        projectile.transform.rotation = Quaternion.identity;
        projectile.SetActive(true);        
    }

}

