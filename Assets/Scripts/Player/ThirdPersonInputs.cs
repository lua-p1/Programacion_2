using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class ThirdPersonInputs : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float sensibilidadX;
    [SerializeField]private float timeToRotate;
    [SerializeField]private PlayerHealth _playerHealth;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField]private float _initHealth;
    [SerializeField]private Slider _healthSlider;
    private Vector2 _getInputs;
    private Vector2 _getMouseInputs;
    private Animator _animator;
    private bool _isDead = false;
    private float currentYRotation = 0f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _playerHealth = new PlayerHealth(_initHealth, _animator,this, _healthSlider);
        _healthSlider.maxValue = _initHealth;
        _healthSlider.value = _initHealth;
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