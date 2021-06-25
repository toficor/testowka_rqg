using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Transform barrelTransform;
    [SerializeField] private PoolManagerData poolManagerData;

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerCombat playerCombat;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerMovement = new PlayerMovement(playerInput, playerData, transform, Camera.main);
        playerCombat = new PlayerCombat(playerData, playerInput, barrelTransform, poolManagerData);
    }

    private void Update()
    {
        playerInput.HandleInput();
        playerMovement.HandleMovement();
        playerCombat.HandleShooting();
    }
}
