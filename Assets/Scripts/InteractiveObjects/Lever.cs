using UnityEngine;
public class Lever : MonoBehaviour, IInteractiveObject
{
    private Animation _anim;
    private bool _canPlay;
    [SerializeField]private Door _doorRef;
    private void Start()
    {
        _canPlay = true;
        _anim = GetComponentInChildren<Animation>();
    }
    public void InteractAction()
    {
        if (!_canPlay && _anim != null) return;
        _anim.Play("LeverOff");
        if (_doorRef != null)
        {
            _doorRef.ToggleDoor();
        }
        _canPlay = false;
        AudioManager.Instance.PlaySoundAtPosition("lever", transform.position);
        Debug.Log("¡Palanca activada!");
    }
}
