using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField]private float _visionRange;
    [SerializeField]private NavMeshAgent _navMeshAgent;
    [SerializeField]private float _attackRange;
    [SerializeField]private float _damage;
    private Vector3 directionToPlayer;
    private Animator _animator;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        Follow();
    }
    private void Follow()
    {
        if (GameManager.instance.player == null) return;
        directionToPlayer = GameManager.instance.player.transform.position - this.transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;
        if (distanceToPlayer <= _visionRange)
        {
            if (distanceToPlayer <= _attackRange)
            {
                _animator.SetBool("EnemyIsAttacking", true);
                _animator.SetBool("EnemyIsWalking", false);
                _navMeshAgent.isStopped = true;
                _navMeshAgent.SetDestination(transform.position);
            }
            else
            {
                _animator.SetBool("EnemyIsAttacking", false);
                _navMeshAgent.isStopped = false;
                Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                _navMeshAgent.SetDestination(GameManager.instance.player.transform.position);
                _animator.SetBool("EnemyIsWalking", true);
            }
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _animator.SetBool("EnemyIsWalking", false);
            _animator.SetBool("EnemyIsAttacking", false);
            _navMeshAgent.isStopped = true;
        }
    }
    public void Attack()
    {
        if (GameManager.instance.player != null)
        {
            var life = GameManager.instance.player.GetComponent<ThridPersonInputs>().GetPlayerComponentLife;
            if (life != null)
            {
                life.TakeDamage(_damage);
            }
            else
            {
                Debug.LogError("No se encontro el componente PlayerHealth en el jugador");
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _visionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, directionToPlayer.normalized * _visionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
