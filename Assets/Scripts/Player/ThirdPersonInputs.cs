using UnityEngine;
using UnityEngine.UI;
public class ThirdPersonInputsw : MonoBehaviour
{
    [SerializeField] private float sensibilidadX;
    [SerializeField] private float timeToRotate;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private float _initHealth;
    [SerializeField] private Slider _healthSlider;
    private Vector2 _getInputs;
    private Vector2 _getMouseInputs;
    private Animator _animator;
    private bool _isDead = false;
    private float currentYRotation = 0f;
    private Rigidbody _rb;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerHealth = new PlayerHealth(_initHealth, _animator, this, _healthSlider);
        _healthSlider.maxValue = _initHealth;
        _healthSlider.value = _initHealth;
    }
    void FixedUpdate()
    {
        if (_isDead) return;
        _getInputs = InputManager.instance.GetMovement();
        _getMouseInputs = InputManager.instance.GetMouseMovement();
        Vector3 direction = new Vector3(_getInputs.x, 0, _getInputs.y).normalized;
        Vector3 move = transform.TransformDirection(direction) * 5f * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + move);
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