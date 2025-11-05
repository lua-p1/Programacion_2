using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolState : IState
{
    private List<Transform> _wayPoints = new List<Transform>();
    private float _minDistanceToWaypoint;
    private Vector3 _currentDestination;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private FSM _fsm;
    private Enemy _enemy;
    private float _visionRange;
    private LayerMask _scenaryMask;
    public PatrolState(float visionRange, NavMeshAgent navMeshAgent, List<Transform> waypoints,float minDistanceToWaypoint,LayerMask scenaryMask, Animator animator,Enemy enemy,FSM fsm)
    {
        _wayPoints = waypoints;
        _minDistanceToWaypoint = minDistanceToWaypoint;
        _navMeshAgent = navMeshAgent;
        _animator = animator;
        _fsm = fsm;
        _enemy = enemy;
        _visionRange = visionRange;
        _scenaryMask = scenaryMask;
    }
    public void OnEnter()
    {
        Debug.Log("Entering Patrol State");
        if (_wayPoints.Count <= 0) return;
        _animator.SetBool("EnemyIsWalking", true);
        SetRandomDestination();
    }

    public void OnExit()
    {
        Debug.Log("Exit Patrol State");
    }
    public void OnUpdate()
    {
        if (_enemy.CheckPlayerDistance() <= _visionRange && LineOfSight.IsOnSight(_enemy.transform.position, GameManager.instance.player.transform.position, _scenaryMask))
        {
            //Debug.Log("veo al PJ");
            _fsm.ChangeState(FSM.State.Chase);
        }
        else
        {
            //Debug.Log("No veo al PJ");
            CheckAndMove();
        }
    }
    private void CheckAndMove()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _minDistanceToWaypoint)
        {
            SetRandomDestination();
        }
    }
    private void SetRandomDestination()
    {
        Transform nextPoint = GetRandomWaypointTransform();
        if (nextPoint != null)
        {
            _currentDestination = nextPoint.position;
            _navMeshAgent.SetDestination(_currentDestination);
        }
    }
    private Transform GetRandomWaypointTransform()
    {
        if (_wayPoints.Count == 0) return null;
        int randomIndex = Random.Range(0, _wayPoints.Count);
        return _wayPoints[randomIndex];
    }
}
