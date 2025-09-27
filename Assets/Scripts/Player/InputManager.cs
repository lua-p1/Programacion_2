using UnityEngine;
public class InputManager : MonoBehaviour
{
    private PlayerControls _playerControls;
    public static InputManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void OnEnable()
    {
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }
    public Vector2 GetMovement()
    {
        return _playerControls.PlayerMovements.Movement.ReadValue<Vector2>();
    }
}