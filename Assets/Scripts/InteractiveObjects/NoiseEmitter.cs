using UnityEngine;
public class NoiseEmitter : MonoBehaviour , IInteractiveObject
{
    [Header("Noise Settings")]
    [SerializeField] public float noiseValue  = 5f;
    [SerializeField] private float activeTime = 5f;
    [SerializeField] private float cooldown = 8f;
    [Header("Feedback")]
    [SerializeField] private GameObject visualFeedback;
    [SerializeField] private string soundName = "noiseEmitter";
    [SerializeField]private bool _active;
    [SerializeField]private bool _onCooldown;
    [SerializeField]private float _timer;
    public bool IsActive => _active;
    public float NoiseAmount => noiseValue ;
    private void Update()
    {
        if (!_active) return;
        _timer += Time.deltaTime;
        if (_timer >= activeTime)
        Deactivate();
    }
    public void Activate()
    {
        Debug.Log("is active)");
        if (_active || _onCooldown) return;
        _active = true;
        _timer = 0f;
        if (visualFeedback != null)
            visualFeedback.SetActive(true);
        AudioManager.Instance.PlaySoundAtPosition(soundName, transform.position);
    }
    public void Consume()
    {
        Destroy(gameObject);
        if (visualFeedback != null)
            visualFeedback.SetActive(false);
        AudioManager.Instance?.StopSound(soundName);
    }
    private void Deactivate()
    {
        _active = false;
        _onCooldown = true;
        if (visualFeedback != null)
            visualFeedback.SetActive(false);
        Invoke(nameof(ResetCooldown), cooldown);
    }
    private void ResetCooldown()
    {
        _onCooldown = false;
    }

    public void InteractAction()
    {
        Activate();
    }
}
