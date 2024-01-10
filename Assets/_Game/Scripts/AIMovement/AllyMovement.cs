using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyMovement : AIMomvement
{

    protected override void Start()
    {
        base.Start();

        targetTag = "Enemy";
        defaultTargetTransform = null;
    }
    protected override void DealDamage()
    {
        target.TryGetComponent(out Stats stats);
        stats.Health -= Damage;
        if (stats.Health <= 0)
        {
            target = FindClosestTarget("Enemy", null);
        }
    }

    protected override void OnMovement()
    {
        if (target == null)
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
}
