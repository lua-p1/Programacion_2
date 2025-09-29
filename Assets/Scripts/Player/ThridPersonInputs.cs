using UnityEngine;

public class ThridPersonInputs : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float sensibilidadX;
    [SerializeField]private float timeToRotate;
    [SerializeField]private PlayerHealth _playerHealth;
    private Vector2 _getInputs;
    private Vector2 _getMouseInputs;
    private Animator animator;
    private float currentYRotation = 0f;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
        _playerHealth = new PlayerHealth(100f);
    }

    void Update()
    {
        _getInputs = InputManager.instance.GetMovement();
        _getMouseInputs = InputManager.instance.GetMouseMovement();
        Vector3 direction = new Vector3(_getInputs.x, 0, _getInputs.y).normalized;
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
        currentYRotation += _getMouseInputs.x * sensibilidadX * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, currentYRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * timeToRotate);
        animator.SetFloat("MovementX", _getInputs.x);
        animator.SetFloat("MovementY", _getInputs.y);
        animator.SetBool("Walk", _getInputs != Vector2.zero);
    }
    public PlayerHealth GetPlayerComponentLife { get => _playerHealth; }
}