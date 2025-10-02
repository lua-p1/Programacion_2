using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PatrolEnemy : MonoBehaviour
{
    [SerializeField]private List<Transform> wayPoints = new List<Transform>();
    [SerializeField]private float _minDistanceToWaypoint = 0.5f;
    [SerializeField]private float _speed = 3;
    private Vector3 _currentDestination;
    [SerializeField]private NavMeshAgent _agent;
    [SerializeField]private Animator _animator;
    private void Start()
    {
        if (wayPoints.Count <= 0)
        {
            Debug.LogError("No hay waypoints asignados. El enemigo no patrullará.");
        }
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _animator.SetBool("EnemyIsWalking", true);
        SetRandomDestination();
    }
    private void Update()
    {
        CheckAndMove();
    }
    private void CheckAndMove()
    {
        if (!_agent.pathPending && _agent.remainingDistance <= _minDistanceToWaypoint)
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
            _agent.SetDestination(_currentDestination);
        }
    }
    private Transform GetRandomWaypointTransform()
    {
        if (wayPoints.Count == 0) return null;
        int randomIndex = Random.Range(0, wayPoints.Count);
        return wayPoints[randomIndex];
    }
}
