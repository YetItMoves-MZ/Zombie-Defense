using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMovement : AIMomvement
{
    public delegate void AllyEvent(AllyMovement allyMovement);
    public AllyEvent OnSummonDeath;
    protected override void Start()
    {
        base.Start();

        targetTag = "Enemy";
        defaultTargetTransform = null;
        myStats.OnDeath += OnAllyDeath;
    }
    protected override void DealDamage()
    {
        GetComponent<AudioSource>().Play();
        target.TryGetComponent(out Stats stats);
        stats.Health -= Damage;
        if (stats.Health <= 0)
        {
            target = FindClosestTarget("Enemy", null);
        }
    }

    protected override void OnMovement()
    {
        if (MovementIndicator())
        {
            agent.destination = AllyMover.Instance.MovementIndicator.position;
            animator.SetBool("IsMoving", true);
        }
        else
        {
            currentMode = Mode.Attacking;
            animator.SetBool("IsMoving", false);
        }
    }

    protected override bool MovementIndicator()
    {
        return target == null;
    }

    void OnAllyDeath()
    {
        OnSummonDeath?.Invoke(this);
    }
}
