using UnityEngine;
public class Lever : MonoBehaviour, IInteractiveObject
{
    private Animator _anim;
    private bool _canPlay;
    [SerializeField] private Door doorRef;
    private void Start()
    {
        _canPlay = true;
        _anim = GetComponentInChildren<Animator>();
    }
    public void InteractAction()
    {
        if (!_canPlay && _anim != null) return;
        _anim.Play("LeverOff");
        if (doorRef != null)
        {
            doorRef.OpenDoor();
        }
        _canPlay = false;
        Debug.Log("¡Palanca activada!");
    }
}
