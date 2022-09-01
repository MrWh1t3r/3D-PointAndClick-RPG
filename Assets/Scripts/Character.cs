using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Character : MonoBehaviour
{
    [Header("Stats")]
    public int curHp;
    public int maxHp;

    [Header("Components")]
    public CharacterController controller;
    protected Character Target;

    public event UnityAction onTakeDamage;

    public void TakeDamage(int damageToTake)
    {
        curHp -= damageToTake;
        onTakeDamage?.Invoke();

        if (curHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void SetTarget(Character t)
    {
        Target = t;
    }
}
