using UnityEngine;
public class ThridPersonInputs : MonoBehaviour
{
    [SerializeField]private float speed;
    [SerializeField]private float sensibilidadX;
    private Vector2 _getInputs;
    private Animator animator;
    public Transform camara;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        _getInputs = InputManager.instance.GetMovement();
        Vector3 direction = new Vector3(_getInputs.x, 0, _getInputs.y).normalized;
        if (direction != Vector3.zero)
        {
            Vector3 camForward = new Vector3(camara.forward.x, 0, camara.forward.z).normalized;
            Vector3 camRight = new Vector3(camara.right.x, 0, camara.right.z).normalized;
            Vector3 moveDir = camForward * direction.z + camRight * direction.x;
            if (direction.z > 0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * sensibilidadX);
            }
            transform.position += moveDir * speed * Time.deltaTime;
        }
        animator.SetFloat("MovementX", Input.GetAxis("Horizontal"));
        animator.SetFloat("MovementY", Input.GetAxis("Vertical"));
        animator.SetBool("Walk", _getInputs != Vector2.zero);
    }
}