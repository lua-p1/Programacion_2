using UnityEngine;
public class PersonajeTercera : MonoBehaviour
{
    public Vector3 movimiento;
    public Rigidbody rb;
    public float speed;
    public Animator animator;
    public Transform domo;
    public float sensibilidadX;
    public float sensibilidadY;
    public float rotationX;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical);
        direction = Vector3.ClampMagnitude(direction, 1);
        movimiento = (transform.right * direction.x) + (transform.forward * direction.z);
        transform.position += movimiento * speed * Time.deltaTime;
        animator.SetFloat("MovementX", horizontal);
        animator.SetFloat("MovementY", vertical);
        bool isWalking = horizontal != 0 || vertical != 0;
        animator.SetBool("Walk", isWalking);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(Vector3.up * mouseX * sensibilidadX * Time.deltaTime);
        rotationX -= mouseY * sensibilidadY;
        rotationX = Mathf.Clamp(rotationX, 20, 20);
        domo.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}