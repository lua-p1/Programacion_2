using UnityEngine;
public class Door : MonoBehaviour
{
    private Animation _anim;
    private bool _isOpen;
    void Start()
    {
        _anim = GetComponentInChildren<Animation>();
        _isOpen = false;
    }
    public void OpenDoor()
    {
        if (_isOpen) return;
        _anim.Play("DoorOpen");
        _isOpen = true;
    }
}
