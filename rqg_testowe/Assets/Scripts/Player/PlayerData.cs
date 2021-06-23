using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float speed;
    public float lives;
    public float shootingDelay;
    public float score;

    public GameObject ammo;
    
}
