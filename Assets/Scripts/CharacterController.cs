using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    private float _moveToUpdateRate = 0.1f;
    private float _lastMoveUpdate;
    private Transform _moveTarget;

    private NavMeshAgent _agent;

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_moveTarget != null && Time.time - _lastMoveUpdate > _moveToUpdateRate)
        {
            _lastMoveUpdate = Time.time;
            MoveToPosition(_moveTarget.position);
        }
    }

    public void LookTowards(Vector3 direction)
    {
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void MoveToTarget(Transform target)
    {
        _moveTarget = target;
    }

    public void MoveToPosition(Vector3 position)
    {
        _agent.isStopped = false;
        _agent.SetDestination(position);
    }

    public void StopMovement()
    {
        _agent.isStopped = true;
        _moveTarget = null;
    }
}
