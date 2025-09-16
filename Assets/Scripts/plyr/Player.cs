using UnityEngine;
public class Player : MonoBehaviour
{
    [SerializeField] private float _initSpeed;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private Movement _movement;
    [SerializeField] private Controller _controller;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _initSpeed = 25f;
    }
    private void Start()
    {
        _movement = new Movement(_initSpeed, _rb);
        _controller = new Controller(_movement);
    }
    private void Update()
    {
        _controller.OnUpdate();
    }
    private void FixedUpdate()
    {
        _controller.OnFixedUpdate();
    }
}