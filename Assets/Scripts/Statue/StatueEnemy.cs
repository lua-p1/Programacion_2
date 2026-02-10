using UnityEngine;
using UnityEngine.AI;
public class StatueEnemy : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float _chaseRange = 8f;
    [Header("Attack")]
    [SerializeField] private float _damage = 20f;
    private Transform _player;
    private ThirdPersonInputs _playerInputs;
    [SerializeField] private Transform lookOrigin;
    private NavMeshAgent _agent;
    [Header("Audio")]
    [SerializeField] private string[] _slideSounds;
    [SerializeField] private float _soundInterval = 2f;
    private float _soundTimer;
    private AudioSourceController _currentAudio;
    private bool _isChasing;
    [Header("Attack")]
    [SerializeField] private float _attackInterval = 1f;
    private float _attackTimer;
    private bool _isAttacking;
    private void Start()
    {
        _player = GameManager.instance.player.transform;
        _playerInputs = _player.GetComponent<ThirdPersonInputs>();
        _agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, _player.position);
        if (distance > _chaseRange)
        {
            Stop();
            _isAttacking = false;
            return;
        }
        if (_playerInputs.IsLookingAtStatue(transform))
        {
            Stop();
            _isAttacking = false;
            return;
        }
        if (distance <= _agent.stoppingDistance + 0.2f)
        {
            AttackPlayer();
            return;
        }
        _isAttacking = false;
        Chase();
        Debug.DrawLine(_player.position + Vector3.up,transform.position + Vector3.up,_playerInputs.IsLookingAtStatue(transform) ? Color.green : Color.red);
    }
    private void Chase()
    {
        _agent.isStopped = false;
        _agent.SetDestination(_player.position);
        if (!_isChasing)
        {
            _isChasing = true;
            _soundTimer = 0f;
            PlayRandomSlide();
        }
        _soundTimer += Time.deltaTime;
        if (_soundTimer >= _soundInterval)
        {
            _soundTimer = 0f;
            PlayRandomSlide();
        }
        if (_currentAudio != null)
            _currentAudio.transform.position = transform.position;
    }
    private void PlayRandomSlide()
    {
        StopSlide();
        int n = Random.Range(0, _slideSounds.Length);
        _currentAudio = AudioManager.Instance.PlayLoopAtPosition(_slideSounds[n], transform.position, AudioManager.Instance.masterVolume, Random.Range(AudioManager.Instance.minPitch, AudioManager.Instance.maxPitch));
    }
    private void Stop()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
        StopSlide();
    }
    private void StopSlide()
    {
        if (_currentAudio != null)
        {
            _currentAudio.Stop();
            _currentAudio = null;
        }
    }
    private void AttackPlayer()
    {
        Stop();
        _isAttacking = true;
        _attackTimer += Time.deltaTime;
        if (_attackTimer >= _attackInterval)
        {
            _attackTimer = 0f;
            var life = _playerInputs.GetPlayerComponentLife;
            life.TakeDamage(_damage);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}