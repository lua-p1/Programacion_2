using Unity.VisualScripting;
using UnityEngine;
public class NoiseEmitter : MonoBehaviour , IInteractiveObject, IAttackable
{
    [Header("Noise Settings")]
    [SerializeField] public float noiseValue  = 5f;
    [SerializeField] private float _activeTime = 5f;
    [SerializeField] private float _cooldown = 8f;
    private AudioSourceController _currentAudio;
    [Header("Feedback")]
    [SerializeField] private GameObject visualFeedback;
    [SerializeField] private string _soundName = "noiseEmitter";
    [SerializeField]private bool _active;
    [SerializeField]private bool _onCooldown;
    [SerializeField]private float _timer;
    public bool IsActive => _active;
    public float NoiseAmount => noiseValue ;
    [Header("Target Point")]
    [SerializeField] private Transform noisePoint;
    public Transform NoisePoint => noisePoint != null ? noisePoint : transform;
    public void OnAttacked(float damage)
    {
        Consume();
    }
    private void Update()
    {
        if (!_active) return;
        if (_currentAudio != null)
            _currentAudio.transform.position = transform.position;
        _timer += Time.deltaTime;
        if (_timer >= _activeTime)
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
        _currentAudio = AudioManager.Instance.PlayLoopAtPosition(_soundName,transform.position,AudioManager.Instance.masterVolume,Random.Range(AudioManager.Instance.minPitch,AudioManager.Instance.maxPitch));
    }
    public void Consume()
    {
        if (_currentAudio != null)
        {
            _currentAudio.Stop();
            _currentAudio = null;
        }
        if (visualFeedback != null)
            visualFeedback.SetActive(false);
        Destroy(gameObject);
    }
    private void Deactivate()
    {
        _active = false;
        _timer = 0f;
        _onCooldown = true;
        if (_currentAudio != null)
        {
            _currentAudio.Stop();
            _currentAudio = null;
        }
        if (visualFeedback != null)
            visualFeedback.SetActive(false);
        Invoke(nameof(ResetCooldown), _cooldown);
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
