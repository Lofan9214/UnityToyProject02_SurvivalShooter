using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public float maxHp = 100f;
    public float Hp { get; private set; }
    public bool IsDead { get; private set; }
    public event Action OnDeath;


    protected virtual void OnEnable()
    {
        IsDead = false;
        Hp = maxHp;
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        Hp -= damage;
        if (Hp <= 0f)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnDeath?.Invoke();
        IsDead = true;
        Hp = 0f;
    }
}
