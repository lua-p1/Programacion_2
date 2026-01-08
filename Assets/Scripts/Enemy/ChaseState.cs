using UnityEngine;
using UnityEngine.AI;
public class ChaseState : IState
{
    [SerializeField] private float _visionRange;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private float _attackRange;
    private Vector3 _directionToPlayer;
    private Animator _animator;
    private Enemy _enemy;
    private FSM _fsm;
    private LayerMask _scenaryMask;
    public ChaseState(float visionRange,NavMeshAgent navMeshAgent,float attackRange,LayerMask scenaryMask, Animator animator,Enemy enemy,FSM fsm)
    {
        _visionRange = visionRange;
        _navMeshAgent = navMeshAgent;
        _attackRange = attackRange;
        _animator = animator;
        _enemy = enemy;
        _fsm = fsm;
        _scenaryMask = scenaryMask;
    }
    public void OnEnter()
    {
        Debug.Log("Entering Chase State");
    }
    public void OnUpdate()
    {
        if (_enemy.CheckPlayerDistance() >= _visionRange && !LineOfSight.IsOnSight(_enemy.transform.position, GameManager.instance.player.transform.position, _scenaryMask))
            _fsm.ChangeState(FSM.State.Patrol);
        Follow();
    }
    private void Follow()
    {
        if (GameManager.instance.player == null) return;
        var playerHealth = GameManager.instance.player.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
        if (playerHealth.GetLife <= 0)
        {
            _animator.SetBool("EnemyIsAttacking", false);
            _animator.SetBool("EnemyIsWalking", false);
            _navMeshAgent.isStopped = true;
            return;
        }
        if (_enemy.CheckPlayerDistance() <= _attackRange)
        {
            _animator.SetBool("EnemyIsAttacking", true);
            _animator.SetBool("EnemyIsWalking", false);
            _navMeshAgent.isStopped = true;
            _navMeshAgent.SetDestination(_enemy.transform.position);
        }
        else
        {
            _directionToPlayer = (GameManager.instance.player.transform.position - _enemy.transform.position).normalized;
            _animator.SetBool("EnemyIsAttacking", false);
            _navMeshAgent.isStopped = false;
            _animator.SetBool("EnemyIsWalking", true);
            Quaternion lookRotation = Quaternion.LookRotation(_directionToPlayer);
            _enemy.transform.rotation = Quaternion.Slerp(_enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
            _navMeshAgent.SetDestination(GameManager.instance.player.transform.position);
        }
    }
    public void OnExit()
    {
        Debug.Log("Exit Chase State");
    }
}
