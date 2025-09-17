using UnityEngine;
public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls;
    public Vector2 movementInput { get; private set; }
    public float verticalInput { get; private set; }
    public float horizontalInput { get; private set; }
    private void Awake()
    {
        _playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.PlayerMovements.Movement.performed += OnMovementPerformed;
        _playerControls.PlayerMovements.Movement.canceled += ctx => movementInput = Vector2.zero;
    }
    private void OnDisable()
    {
        _playerControls.PlayerMovements.Movement.performed -= OnMovementPerformed;
        _playerControls.Disable();
    }
    private void OnMovementPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        movementInput = ctx.ReadValue<Vector2>();
    }
    public void HandleAllInputs()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;
    }
}
