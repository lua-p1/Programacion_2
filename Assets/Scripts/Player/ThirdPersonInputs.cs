using UnityEngine;
using UnityEngine.UI;
public class ThirdPersonInputs : MonoBehaviour, IAttackable
{
    [SerializeField]private float sensibilidadX;
    [SerializeField]private float timeToRotate;
    [SerializeField]private PlayerHealth _playerHealth;
    [SerializeField]private float _initHealth;
    [SerializeField]private Slider _healthSlider;
    [SerializeField]private ParticleSystem _damageParticles;
    private Vector2 _getInputs;
    private Vector2 _getMouseInputs;
    private Animator _animator;
    private bool _isDead = false;
    private float currentYRotation = 0f;
    private Rigidbody _rb;
    [Header("Noise")]
    [SerializeField] private float _currentNoise = 0;
    [SerializeField] private float _minWalkNoise = 1f;
    [SerializeField] private float _maxWalkNoise = 2f;
    [SerializeField] private float _noiseBuildUpSpeed = 0.25f;
    [SerializeField] private float _noiseDecaySpeed = 0.5f;
    [Header("Noise UI")]
    [SerializeField] private Slider _noiseSlider;
    private PlayerNoise _playerNoise;
    public float CurrentNoise => _playerNoise.CurrentNoise;
    [Header("Look Detection")]
    [SerializeField] private Transform _lookOrigin;
    [SerializeField] private float _lookMaxDistance = 45f;
    [SerializeField] private float _lookMaxAngle = 20;
    [SerializeField] private LayerMask _statueLayerMask;
    [Header("Attack Point")]
    [SerializeField] private Transform attackPoint;
    public Transform AttackPoint => attackPoint;
    public void OnAttacked(float damage)
    {
        GetPlayerComponentLife.TakeDamage(damage);
    }
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _playerHealth = new PlayerHealth(_initHealth, _animator, this, _healthSlider, _damageParticles);
        _healthSlider.maxValue = _initHealth;
        _healthSlider.value = _initHealth;
        _playerNoise = new PlayerNoise(_minWalkNoise,_maxWalkNoise,_noiseBuildUpSpeed,_noiseDecaySpeed,_noiseSlider);
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
        _playerNoise.UpdateNoise(_getInputs != Vector2.zero, Time.fixedDeltaTime);
    }
    public bool IsLookingAtStatue(Transform statue)
    {
        if (_lookOrigin == null) return false;
        Vector3 origin = _lookOrigin.position;
        Vector3 forward = _lookOrigin.forward;
        Vector3 dirToStatue = (statue.position - origin).normalized;
        float angle = Vector3.Angle(forward, dirToStatue);
        if (angle > _lookMaxAngle/2) return false;
        if (Physics.Raycast(origin, dirToStatue, out RaycastHit hit, _lookMaxDistance, _statueLayerMask))
        {
            return hit.transform.GetComponentInParent<StatueEnemy>() != null;
        }
        return false;
    }
    public void OnDeath()
    {
        _currentNoise = 0;
        _isDead = true;
        _playerNoise.ResetNoise();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public PlayerHealth GetPlayerComponentLife { get => _playerHealth; }
    private void OnDrawGizmos()
    {
        if (_lookOrigin == null) return;
        Vector3 origin = _lookOrigin.position;
        Vector3 forward = _lookOrigin.forward;
        float halfAngle = _lookMaxAngle * 0.5f;
        float distance = _lookMaxDistance;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(origin, forward * distance);
        Vector3 rightLimit = Quaternion.AngleAxis(halfAngle, Vector3.up) * forward;
        Vector3 leftLimit = Quaternion.AngleAxis(-halfAngle, Vector3.up) * forward;
        Gizmos.DrawRay(origin, rightLimit * distance);
        Gizmos.DrawRay(origin, leftLimit * distance);
    }
}