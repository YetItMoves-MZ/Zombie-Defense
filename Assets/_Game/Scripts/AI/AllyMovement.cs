using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMovement : AIMomvement
{
    public delegate void AllyEvent(AllyMovement allyMovement);
    public AllyEvent OnSummonDeath;

    int soldierUpgrade = 0;
    protected override void Start()
    {
        base.Start();

        targetTag = "Enemy";
        defaultTargetTransform = null;
        myStats.OnDeath += OnAllyDeath;
        soldierUpgrade = BuildingUpgrades.Instance.Soldier.UpgradeValue;
        BuildingUpgrades.Instance.Soldier.UpgradeValueChanged += OnUpgradeValueChange;
    }

    private void OnUpgradeValueChange(int newValue)
    {
        myStats.MaxHealth += newValue - soldierUpgrade;
        soldierUpgrade = newValue;
    }

    protected override void DealDamage()
    {
        transform.LookAt(target);
        GetComponent<ParticleShooter>().ShootParticle();
        GetComponent<AudioSource>().Play();
        if (!target.TryGetComponent(out Stats stats))
            return;
        stats.Health -= Damage + BuildingUpgrades.Instance.Soldier.UpgradeValue;
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
        BuildingUpgrades.Instance.Soldier.UpgradeValueChanged -= OnUpgradeValueChange;
        OnSummonDeath?.Invoke(this);
    }

}
