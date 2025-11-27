using System.Collections.Generic;
using UnityEngine;
public class WayPointsManager : MonoBehaviour
{
    [SerializeField] private List<Transform> wayPoints = new List<Transform>();
    [SerializeField] private float _minDistanceToWaypoint = 0.5f;
    [SerializeField] private float _speed = 3;
    private Vector3 _currentDestination;
    private void Start()
    {
        if (wayPoints.Count < 0)
        {
            return;
        }
        SetRandomDestination();
    }
    private void Update()
    {
        MoveTowardsDestination();
    }
    private void MoveTowardsDestination()
    {
        var distance = _currentDestination - transform.position;
        if (distance.magnitude < _minDistanceToWaypoint)
        {
            SetRandomDestination();
        }
        Vector3 direction = (_currentDestination - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void SetRandomDestination()
    {
        var nextPoint = GetRandomWaypointTransform();
        if (nextPoint != null)
        {
            _currentDestination = nextPoint.position;
        }
    }
    private Transform GetRandomWaypointTransform()
    {
        if (wayPoints.Count == 0) return null;
        int randomIndex = Random.Range(0, wayPoints.Count);
        return wayPoints[randomIndex];
    }
}