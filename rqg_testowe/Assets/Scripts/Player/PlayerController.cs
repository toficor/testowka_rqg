using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private GameObject barrierObject;
    [SerializeField] private PoolManagerData poolManagerData;

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;
    private PlayerHealth playerHealth;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = new PlayerMovement(playerInput, playerData, transform, Camera.main);
        playerCombat = new PlayerCombat(playerData, playerInput, barrelTransform, poolManagerData);
        playerHealth = new PlayerHealth(playerData, barrierObject);
        playerData.OnPlayerDead += KillPlayer;
    }

    private void OnEnable()
    {
        playerData.lives = 3;
    }

    private void Update()
    {
        playerInput.HandleInput();
        playerMovement.HandleMovement();
        playerCombat.HandleShooting();
        playerHealth.HandleBarrier();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            playerHealth.HandleHit();
        }
    }

    private void KillPlayer()
    {
        gameObject.SetActive(false);
    }
}
