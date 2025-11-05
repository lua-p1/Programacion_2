using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private Animator _animator;
    private FSM _fsm;
    [SerializeField]private float _visionRange;
    [Header("Chase")]
    [SerializeField]private NavMeshAgent _navMeshAgent;
    [SerializeField]private float _attackRange;
    [SerializeField]private float _damage;
    [Header("Patrol")]
    [SerializeField]private List<Transform> wayPoints = new List<Transform>();
    [SerializeField]private float _minDistanceToWaypoint = 0.5f;
    [SerializeField]private LayerMask _scenaryMask;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        _fsm = new FSM();
        _fsm.AddState(FSM.State.Chase, new ChaseState(_visionRange, _navMeshAgent, _attackRange, _scenaryMask, _animator, this, _fsm));
        _fsm.AddState(FSM.State.Patrol, new PatrolState(_visionRange, _navMeshAgent, wayPoints, _minDistanceToWaypoint, _scenaryMask, _animator,this,_fsm));
        _fsm.ChangeState(FSM.State.Patrol);
    }
    private void Update()
    {
        //print(LineOfSight.IsOnSight(this.transform.position, GameManager.instance.player.transform.position, _scenaryMask));
        _fsm.OnUpdate();
    }
    public float CheckPlayerDistance()
    {
        var _distanceToPlayer = GameManager.instance.player.transform.position - this.transform.position;
        return _distanceToPlayer.magnitude;
    }
    public void Attack()
    {
        if (GameManager.instance.player != null)
        {
            var life = GameManager.instance.player.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, GameManager.instance.player.transform.position);
    }
}
