using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
    private PlayerInput playerInput;
    private PlayerData playerData;
    private Transform playerTransform;
    private Vector3 screenBounds;

    public PlayerMovement( PlayerInput playerInput, PlayerData playerData, Transform playerTransform, Camera camera)
    {
        this.playerInput = playerInput;
        this.playerData = playerData;
        this.playerTransform = playerTransform;
        this.screenBounds = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.y));
    }


    public void HandleMovement()
    {
        playerTransform.position += new Vector3(playerInput.Horizontal * playerData.speed * Time.deltaTime, 0f, 0f);
        playerTransform.position = new Vector3(Mathf.Clamp(playerTransform.position.x, -screenBounds.x, screenBounds.x), 0f, playerTransform.position.z);
    }


}
