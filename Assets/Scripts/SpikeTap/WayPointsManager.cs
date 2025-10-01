using System.Collections.Generic;
using UnityEngine;
public class WayPointsManager : MonoBehaviour
{
    [SerializeField]private List<Transform> wayPoints = new List<Transform>();
    [SerializeField]private int _wayPointIndex;
    [SerializeField]private bool _isMoving;
    [SerializeField]private float _speed;
    [SerializeField]private float _rotSpeed;
    [SerializeField]private bool _isLooping;
    [SerializeField]private bool _isRandom;
    private void Start()
    {
        Patrol();
    }
    private void Patrol()
    {
        _wayPointIndex = 0;
        //_isMoving = true;
    }
    private void Update()
    {
        if (!_isMoving)return;
        if (_wayPointIndex < wayPoints.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[_wayPointIndex].position, Time.deltaTime * _speed);
            var _direction = transform.position - wayPoints[_wayPointIndex].position;
            var _targetRot = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRot, _rotSpeed * Time.deltaTime);
            var _distance = Vector3.Distance(transform.position, wayPoints[_wayPointIndex].position);
            if (_distance <= 0.05f)

                if(_isRandom){
                    _wayPointIndex = Random.Range(0, wayPoints.Count);
                }
                else
                {
                    _wayPointIndex++;
                    if (_isLooping && _wayPointIndex >= wayPoints.Count)
                    {
                        _wayPointIndex = 0;
                    }
                }               
            }
        }
}
