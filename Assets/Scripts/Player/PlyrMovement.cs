using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class PlyrMovement : MonoBehaviour
{
    private InputManager _inputManager;
    private Vector3 _moveDirection;
    private Transform _cameraObj;
    private Rigidbody _rb;
    [SerializeField]private float _speed;
    [SerializeField]private float _rotSpeed;
    public void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
        _cameraObj = Camera.main.transform;
    }
    public void Start()
    {
        _speed = 10f;
        _rotSpeed = 10f;
    }
    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        _moveDirection = _cameraObj.forward * _inputManager.verticalInput;
        _moveDirection = _moveDirection + _cameraObj.right * _inputManager.horizontalInput;
        _moveDirection.Normalize();
        _moveDirection.y = 0;
        _moveDirection = _moveDirection * _speed;
        Vector3 movVelocity = _moveDirection;
        _rb.velocity = movVelocity;
    }
    private void HandleRotation()
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = _cameraObj.forward * _inputManager.verticalInput;
        targetDirection = targetDirection + _cameraObj.right * _inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion plyrRotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotSpeed * Time.deltaTime);
        transform.rotation = plyrRotation;
    }
}
