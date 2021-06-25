using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth
{
    private PlayerData playerData;
    private GameObject barrierObject;
    private float invincibleTimer;

    public PlayerHealth(PlayerData playerData, GameObject barrierObject)
    {
        this.playerData = playerData;
        this.invincibleTimer = playerData.invincibleTime;
        this.barrierObject = barrierObject;
    }

    public void HandleHit()
    {        
        if(invincibleTimer < playerData.invincibleTime)
        {
            return;
        }

        playerData.OnPlayerGetHit?.Invoke();
        invincibleTimer = 0f;
        playerData.lives--;

        HandleDeath();
    }

    public void HandleBarrier()
    {
        invincibleTimer += Time.deltaTime;
        if(invincibleTimer <= playerData.invincibleTime)
        {
            barrierObject.gameObject.SetActive(true);
        }
        else
        {
            barrierObject.gameObject.SetActive(false);
        }
    }

    public void HandleDeath()
    {
        if(playerData.lives <= 0)
        {
            playerData.OnPlayerDead?.Invoke();
        }
    }

}
