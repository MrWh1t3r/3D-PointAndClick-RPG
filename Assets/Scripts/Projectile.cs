using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime = 5.0f;

    private GameObject _target;
    private Character _owner;

    private Rigidbody _rig;

    private void Start()
    {
        _rig = GetComponent<Rigidbody>();
        _rig.velocity = transform.forward * moveSpeed;
        
        Destroy(gameObject,lifetime);
    }

    public void Setup(Character character)
    {
        _owner = character;
    }

    private void OnTriggerEnter(Collider other)
    {
        Character hit = other.GetComponent<Character>();
        
        if(hit!=_owner && hit!=null)
        {
            hit.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
