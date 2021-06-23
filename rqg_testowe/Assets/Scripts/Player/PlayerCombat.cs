using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat
{
    private Transform barrelTransform;
    private PlayerData playerData;
    private PlayerInput playerInput;

    private float shootingTimmer;

    public PlayerCombat(PlayerData playerData, PlayerInput playerInput, Transform barellTransform)
    {
        this.barrelTransform = barellTransform;
        this.playerData = playerData;
        this.playerInput = playerInput;
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
        GameObject.Instantiate(playerData.ammo, new Vector3(barrelTransform.position.x, barrelTransform.position.y, barrelTransform.position.z), Quaternion.identity);
    }

}

