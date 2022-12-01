using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private Transform _waypointsRoot;
    [SerializeField] private float _speed = 2.0f;

    private Animator _animator;
    private Waypoint[] _waypoints;
    private int _currentWaypointIndex = 0;
    private float _destinationThreshold = 0.1f;
    private float _idleSpeed = 0.0f;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _waypoints = _waypointsRoot.GetComponentsInChildren<Waypoint>();
    }

    private void Update()
    {
        FindNextWaypoint();

        if (_waypoints.Length > 0)
        {
            Vector2 waypointPosition = NormalizeWaypointPositionToPlayerPosition(_waypoints[_currentWaypointIndex].transform.position);
            transform.position = Vector2.MoveTowards(transform.position, waypointPosition, _speed * Time.deltaTime);
        }
        else
        {
            _speed = _idleSpeed;
        }

        _animator.SetBool(AngryPigAnimatorController.Params.Run, _speed > _idleSpeed);
    }

    private void FindNextWaypoint()
    {
        if (_waypoints.Length > 0)
        {
            Vector2 waypointPosition = NormalizeWaypointPositionToPlayerPosition(_waypoints[_currentWaypointIndex].transform.position);
            float distanceToCurrentWaypoint = Vector2.Distance(waypointPosition, transform.position);

            if (distanceToCurrentWaypoint <= _destinationThreshold)
            {
                _currentWaypointIndex++;

                if (_currentWaypointIndex >= _waypoints.Length)
                {
                    _currentWaypointIndex = 0;
                }
            }
        }
    }

    private Vector2 NormalizeWaypointPositionToPlayerPosition(Vector2 position)
    {
        return new Vector2(position.x, transform.position.y);
    }
}
