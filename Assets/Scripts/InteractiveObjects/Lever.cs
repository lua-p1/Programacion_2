using UnityEngine;
public class Lever : MonoBehaviour, IInteractiveObject
{
    private Animation _anim;
    private bool _canPlay;
    [SerializeField] private Door doorRef;
    private void Start()
    {
        _canPlay = true;
        //_anim = GetComponentInChild<Animation>();
    }
    public void InteractAction()
    {
        if (!_canPlay) return;
        _anim.Play("LeverOff");
        if (doorRef != null)
        {
            doorRef.OpenDoor();
        }
        _canPlay = false;
        Debug.Log("¡Palanca activada!");
    }
}
