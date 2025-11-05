using UnityEngine;
public class Door : MonoBehaviour
{
    private Animator _anim;
    private bool _isOpen;
    void Start()
    {
        _isOpen = false;
        _anim = GetComponent<Animator>();
    }
    public void OpenDoor()
    {
        if (_isOpen) return;
        _anim.Play("DoorOpen");
        _isOpen = true;
    }
}
