using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if (value <= 0)
                OnDeath();
        }
    }
    [SerializeField] int maxHealth;

    private void OnDeath()
    {
        gameObject.tag = "Dead";
        if (TryGetComponent(out ZombieMovement zombieMovement))
            zombieMovement.OnDeath();
        else
            Destroy(gameObject, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
