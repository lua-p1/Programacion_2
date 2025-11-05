using UnityEngine;
public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    private PlayerControls _playerControls;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }
    public Vector2 Movement => _playerControls.PlayerMovements.Movement.ReadValue<Vector2>();
    public Vector2 MouseMovement => _playerControls.PlayerMovements.MouseMovement.ReadValue<Vector2>();
}
