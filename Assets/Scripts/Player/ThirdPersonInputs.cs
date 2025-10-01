using UnityEngine;
public class ThirdPersonInputs : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float sensibilidadX;
    [SerializeField]private float timeToRotate;
    [SerializeField]private PlayerHealth _playerHealth;
    [SerializeField] private float _initHealth;
    private Vector2 _getInputs;
    private Vector2 _getMouseInputs;
    private Animator _animator;
    private bool _isDead = false;
    private float currentYRotation = 0f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponentInChildren<Animator>();
        _playerHealth = new PlayerHealth(_initHealth, _animator,this);
    }
    void Update()
    {
        if (_isDead) return;
        _getInputs = InputManager.instance.GetMovement();
        _getMouseInputs = InputManager.instance.GetMouseMovement();
        Vector3 direction = new Vector3(_getInputs.x, 0, _getInputs.y).normalized;
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
        currentYRotation += _getMouseInputs.x * sensibilidadX * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, currentYRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * timeToRotate);
        _animator.SetFloat("MovementX", _getInputs.x);
        _animator.SetFloat("MovementY", _getInputs.y);
        _animator.SetBool("Walk", _getInputs != Vector2.zero);
    }
    public void OnDeath()
    {
        _isDead = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public PlayerHealth GetPlayerComponentLife { get => _playerHealth; }
}