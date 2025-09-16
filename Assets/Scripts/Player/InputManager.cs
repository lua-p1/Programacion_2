using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    public Vector2 _movementInput;
    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();
            playerControls.PlayerMovements.Movement.performed += i => _movementInput = i.ReadValue<Vector2>();

        }
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }
}
