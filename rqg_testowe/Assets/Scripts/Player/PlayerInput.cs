using UnityEngine;

public class PlayerInput
{

    public float Horizontal { get; private set; }
    public bool IsShooting { get; private set; }


    public void HandleInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        IsShooting = Input.GetButton("Jump");
    }

}
