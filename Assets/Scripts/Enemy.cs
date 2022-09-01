using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public enum State
    {
        Idle,
        Chase,
        Attack
    }

    private State curState = State.Idle;

    [Header("Ranges")]
    [SerializeField] private float chaseRange;
    [SerializeField] private float attackRange;

    [Header("Attack")]
    [SerializeField] private float attackRate;
    [SerializeField] private GameObject attackPrefab;
    private float _lastAttackTime;

    private float _targetDistance;

    private void Start()
    {
        Target = Player.Current;
    }

    private void Update()
    {
        if(Target==null)
            return;
        
        _targetDistance = Vector3.Distance(transform.position, Target.transform.position);
        
        switch (curState)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Attack:
                AttackUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
        }
    }

    void SetState(State newState)
    {
        curState = newState;

        switch (curState)
        {
            case State.Idle:
                controller.StopMovement();
                break;
            case State.Chase:
                controller.MoveToTarget(Target.transform);
                break;
            case State.Attack:
                controller.StopMovement();
                break;
        }
    }

    void IdleUpdate()
    {
        if(_targetDistance <chaseRange && _targetDistance>attackRange)
            SetState(State.Chase);
        else if (_targetDistance<attackRange)
            SetState(State.Attack);
    }

    void ChaseUpdate()
    {
        if(_targetDistance>chaseRange)
            SetState(State.Idle);
        else if(_targetDistance < attackRange)
            SetState(State.Attack);
    }

    void AttackUpdate()
    {
        if(_targetDistance>attackRange)
            SetState(State.Chase);
        
        controller.LookTowards(Target.transform.position - transform.position);

        if (Time.time - _lastAttackTime > attackRate)
        {
            _lastAttackTime = Time.time;
            AttackTarget();
        }
    }

    void AttackTarget()
    {
        
    }
}
