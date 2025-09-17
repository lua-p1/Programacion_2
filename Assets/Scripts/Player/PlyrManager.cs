using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyrManager : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private PlyrMovement _plyrMovement;
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _plyrMovement = GetComponent<PlyrMovement>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        _inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        _plyrMovement.HandleAllMovement();
    }
}
