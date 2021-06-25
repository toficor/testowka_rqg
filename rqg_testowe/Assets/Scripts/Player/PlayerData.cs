using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float speed;
    public float lives;
    public float shootingDelay;
    public float invincibleTime;

    [HideInInspector] public bool enableMoving;
    [HideInInspector] public bool enableShooting;


    public Action OnPlayerGetHit;
    public Action OnPlayerDead;
    
}
