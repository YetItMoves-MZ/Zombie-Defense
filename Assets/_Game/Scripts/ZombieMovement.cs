using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    enum Mode
    {
        Idle,
        Moveing,
        Attacking,
        Dead
    }

    public float AttackRange = 1f;
    public float SightRange;
    public int Damage;

    NavMeshAgent agent;
    private Transform target;
    Mode currentMode;
    Animator animator;
    bool isAttackAnimationStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMode == Mode.Idle)
            OnIdle();
        if (currentMode == Mode.Moveing)
            OnMovement();
        if (currentMode == Mode.Attacking)
            OnAttacking();
    }

    private void OnIdle()
    {
        target = FindClosestTarget();
        float targetDistance = Vector3.Distance(target.position, transform.position);
        if (targetDistance > AttackRange)
            currentMode = Mode.Moveing;
        else
            currentMode = Mode.Attacking;

    }

    private Transform FindClosestTarget()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, SightRange, transform.forward, SightRange);

        if (hits.Length < 1)
            return MainBase.Instance.transform;

        Transform closest = hits[0].transform;
        float closestRange = -1;
        foreach (RaycastHit hit in hits)
        {
            if (closestRange < 0)
            {
                if (hit.transform.tag == "Ally")
                {
                    closest = hit.transform;
                    closestRange = Vector3.Distance(closest.position, transform.position);
                    continue;
                }
            }
            else
            {
                float hitRange = Vector3.Distance(hit.transform.position, transform.position);
                if (hitRange < closestRange)
                {
                    if (hit.transform.tag == "Ally")
                    {
                        closest = hit.transform;
                        closestRange = hitRange;
                    }
                }
            }
        }
        return closestRange < 0 ? MainBase.Instance.transform : closest;

    }

    private void OnMovement()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);
        if (targetDistance > AttackRange)
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

    private void OnAttacking()
    {
        float targetDistance = Vector3.Distance(target.position, transform.position);
        if (targetDistance <= AttackRange)
        {
            agent.destination = transform.position;
            animator.SetBool("CanAttack", true);
            if (isAttackAnimationStarted && !animator.GetBool("IsAttacking"))
            {
                DealDamage();
                isAttackAnimationStarted = false;
            }
            if (animator.GetBool("IsAttacking"))
                isAttackAnimationStarted = true;
        }
        else
        {
            currentMode = Mode.Moveing;
            animator.SetBool("CanAttack", false);
        }
    }

    private void DealDamage()
    {
        target.TryGetComponent(out Stats stats);
        stats.Health -= Damage;
        if (stats.Health <= 0)
        {
            target = FindClosestTarget();
        }

    }

    public void OnDeath()
    {
        currentMode = Mode.Dead;
        animator.SetBool("IsDead", true);
        StartCoroutine(WaitForDestruction());
    }

    IEnumerator WaitForDestruction()
    {
        bool isWaiting = true;
        while (isWaiting)
        {
            if (animator.GetBool("DestroyObject"))
            {
                Destroy(gameObject, 2f);
                isWaiting = false;
            }
            yield return null;
        }
    }
}
