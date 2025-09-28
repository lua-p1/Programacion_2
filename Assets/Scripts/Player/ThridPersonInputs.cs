using UnityEngine;
public class ThridPersonInputs : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float sensibilidadX;
    [SerializeField]private float timeToRotate;
    private Vector2 _getInputs;
    private Vector2 _getMouseInputs;
    private Animator animator;
    private float currentYRotation = 0f;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        _getInputs = InputManager.instance.GetMovement();
        _getMouseInputs = InputManager.instance.GetMouseMovement();
        Vector3 direction = new Vector3(_getInputs.x, 0, _getInputs.y).normalized;
        transform.position += direction * speed * Time.deltaTime;
        currentYRotation += _getMouseInputs.x * sensibilidadX * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, currentYRotation, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * timeToRotate);
        animator.SetFloat("MovementX", Input.GetAxis("Horizontal"));
        animator.SetFloat("MovementY", Input.GetAxis("Vertical"));
        animator.SetBool("Walk", _getInputs != Vector2.zero);
    }
}