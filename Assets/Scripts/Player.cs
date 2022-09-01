using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float attackRange;
    [SerializeField] private float attackRate;
    private float _lastAttackTime;
    [SerializeField] private GameObject attackPrefab;
    
    public static Player Current;

    private void Awake()
    {
        Current = this;
    }

    private void Update()
    {
        if (Target != null)
        {
            float targetDistance = Vector3.Distance(transform.position, Target.transform.position);

            if (targetDistance < attackRange)
            {
                controller.StopMovement();
                controller.LookTowards(Target.transform.position - transform.position);

                if (Time.time - _lastAttackTime > attackRate)
                {
                    _lastAttackTime = Time.time;
                    GameObject proj = Instantiate(attackPrefab, transform.position + Vector3.up,
                        Quaternion.LookRotation(Target.transform.position - transform.position, Vector3.up));
                    proj.GetComponent<Projectile>().Setup(this);
                }
            }
            else
            {
                controller.MoveToTarget(Target.transform);
            }
        }
    }
}
