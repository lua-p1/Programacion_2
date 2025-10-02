using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WayPointsManager : MonoBehaviour
{
    [SerializeField]private List<Transform> wayPoints = new List<Transform>();
    [SerializeField]private int _wayPointIndex;
    [SerializeField]private bool _isMoving = false;
    [SerializeField]private bool _isLooping;
    [SerializeField]private bool _isRandom;
    [SerializeField]private float _minDistanceToWaypoint = 0.5f;
    private NavMeshAgent _agent;
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (wayPoints.Count > 0)
        {
            _wayPointIndex = 0;
            SetNextDestination();
        }
    }
    private void Update()
    {
        if (!_isMoving || wayPoints.Count == 0) return;
        if (!_agent.pathPending && _agent.remainingDistance <= _minDistanceToWaypoint)
        {
            if (_isRandom)
            {
                _wayPointIndex = Random.Range(0, wayPoints.Count);
            }
            else
            {
                _wayPointIndex++;
                if (_isLooping && _wayPointIndex >= wayPoints.Count)
                {
                    _wayPointIndex = 0;
                }
                else if (_wayPointIndex >= wayPoints.Count)
                {
                    _isMoving = false;
                    return;
                }
            }
            SetNextDestination();
        }
    }
    private void SetNextDestination()
    {
        if (wayPoints.Count == 0) return;
        _agent.SetDestination(wayPoints[_wayPointIndex].position);
    }
}
