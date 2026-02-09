using UnityEngine;
using UnityEngine.AI;
public class StatueEnemy : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float _chaseRange = 6f;
    [Header("Attack")]
    [SerializeField] private float _damage = 10f;
    private Transform _player;
    private ThirdPersonInputs _playerInputs;
    [SerializeField] private Transform lookOrigin;
    private NavMeshAgent _agent;
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
            return;
        }
        if (_playerInputs.IsLookingAtStatue(transform))
        {
            Stop();
            return;
        }
        Chase();
        Debug.DrawLine(_player.position + Vector3.up,transform.position + Vector3.up,_playerInputs.IsLookingAtStatue(transform) ? Color.green : Color.red);
    }
    private void Chase()
    {
        _agent.isStopped = false;
        _agent.updateRotation = true;
        _agent.SetDestination(_player.position);
    }
    private void Stop()
    {
        _agent.isStopped = true;
        _agent.velocity = Vector3.zero;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var life = other.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
        life.TakeDamage(_damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}