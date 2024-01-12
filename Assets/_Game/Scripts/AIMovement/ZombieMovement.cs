using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : AIMomvement
{

    public int MoneyGainedOnKilled;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        targetTag = "Ally";
        defaultTargetTransform = MainBase.Instance.transform;

        DayNightCycle.Instance.DayFunctions += OnDeath; // kill zombie when day starts
        BuffZombieByDayCounter();
        myStats.OnDeath += OnZombieDeath;
    }

    void OnZombieDeath()
    {
        MoneyManager.Instance.GainMoney(MoneyGainedOnKilled);
    }

    void BuffZombieByDayCounter()
    {
        Damage = (int)(Damage + 0.5f * DayNightCycle.Instance.DayCounter);
        myStats.MaxHealth = (int)(myStats.MaxHealth + DayNightCycle.Instance.DayCounter);
        myStats.FullHeal();
    }
    protected override void OnMovement()
    {
        if (MovementIndicator())
        {
            agent.destination = target.position;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            currentMode = Mode.Attacking;
            animator.SetBool("IsMoving", false);
        }
    }

    protected override void DealDamage()
    {
        target.TryGetComponent(out Stats stats);
        stats.Health -= Damage;
        if (stats.Health <= 0)
        {
            target = FindClosestTarget("Ally", MainBase.Instance.transform);
        }
    }

    protected override bool MovementIndicator()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);
        return targetDistance > AttackRange;
    }
}
