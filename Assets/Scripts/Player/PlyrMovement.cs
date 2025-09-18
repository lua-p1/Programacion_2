using UnityEngine;
public class PlyrMovement : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody _rb;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _rotSpeed = 10f;
    private Vector3 _targetDirection;
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
    public void HandleAllMovement()
    {
        CalculateDirection();
        HandleMovement();
        HandleRotation();
    }
    private void CalculateDirection()
    {
        _targetDirection = transform.forward * _inputManager.verticalInput + transform.right * _inputManager.horizontalInput;
        _targetDirection.Normalize();
    }
    private void HandleMovement()
    {
        Vector3 velocity = _targetDirection * _speed;
        velocity.y = _rb.velocity.y;
        _rb.velocity = velocity;
    }
    private void HandleRotation()
    {
        if (_targetDirection.sqrMagnitude > 0.001f && _inputManager.verticalInput > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotSpeed * Time.deltaTime);
        }
    }
}
