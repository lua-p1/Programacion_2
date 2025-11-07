using UnityEngine;
public class Door : MonoBehaviour
{
    [SerializeField]private float _openAngle = 90f;
    [SerializeField]private float _speed = 2f;
    private bool _isOpen = false;
    private Quaternion _closedRot;
    private Quaternion _openRot;
    void Start()
    {
        _closedRot = transform.localRotation;
        _openRot = Quaternion.Euler(transform.localEulerAngles + new Vector3(0f, _openAngle, 0f));
    }
    void Update()
    {
        if (_isOpen)
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _openRot, Time.deltaTime * _speed);
        else
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _closedRot, Time.deltaTime * _speed);
    }
    public void ToggleDoor()
    {
        _isOpen = !_isOpen;
    }
}
