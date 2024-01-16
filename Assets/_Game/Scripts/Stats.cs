using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public delegate void VoidEvent();
    public VoidEvent OnHealthChanged;
    public VoidEvent OnDeath;
    int health;
    bool isDead;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            OnHealthChanged?.Invoke();
            if (value <= 0 && !isDead)
            {
                isDead = true;
                OnDeath?.Invoke();
            }
        }
    }
    public int MaxHealth;

    private void DefaultDeath()
    {
        gameObject.tag = "Dead";
    }

    void Start()
    {
        isDead = false;
        Health = MaxHealth;
        OnDeath += DefaultDeath;
    }

    public void FullHeal()
    {
        Health = MaxHealth;
    }

    private void OnDestroy()
    {
        OnHealthChanged = null;
        OnDeath = null;
    }
}
