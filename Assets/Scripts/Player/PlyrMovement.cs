using UnityEngine;
public class PlyrMovement : MonoBehaviour
{
    private InputManager _inputManager;
    private Transform _cameraObj;
    private Rigidbody _rb;
    [SerializeField]private float _speed;
    [SerializeField]private float _rotSpeed;
    private Vector3 _targetDirection;
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
        _cameraObj = Camera.main.transform;
    }
    private void Start()
    {
        _speed = 10f;
        _rotSpeed = 10f;
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
        _targetDirection = _cameraObj.forward * _inputManager.verticalInput + _cameraObj.right * _inputManager.horizontalInput;
        _targetDirection.y = 0;
        _targetDirection.Normalize();
    }
    private void HandleMovement()
    {
        Vector3 targetVelocity = _targetDirection * _speed;
        _rb.velocity = targetVelocity;
    }
    private void HandleRotation()
    {
        if (_targetDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotSpeed * Time.deltaTime);
        }
    }
}