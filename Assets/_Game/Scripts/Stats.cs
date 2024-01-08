using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public delegate void VoidEvent();
    public VoidEvent OnDamageTaken;
    public VoidEvent OnDeath;
    int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            if (health > value)
                OnDamageTaken?.Invoke();
            health = value;
            if (value <= 0)
                OnDeath?.Invoke();
        }
    }
    [SerializeField] int maxHealth;

    private void DefaultDeath()
    {
        gameObject.tag = "Dead";
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
        OnDeath += DefaultDeath;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
