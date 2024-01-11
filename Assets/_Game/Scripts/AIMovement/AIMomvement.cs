using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AIMomvement : MonoBehaviour
{
    protected enum Mode
    {
        Idle,
        Moveing,
        Attacking,
        Dead
    }

    public float AttackRange = 1f;
    public float SightRange;
    public int Damage;

    protected NavMeshAgent agent;
    protected Transform target;
    protected Mode currentMode;
    protected Animator animator;
    protected bool isAttackAnimationStarted = false;
    protected Stats myStats;

    protected string targetTag = "";
    protected Transform defaultTargetTransform = null;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        myStats = GetComponent<Stats>();
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();

        currentMode = Mode.Idle;

        myStats.OnDeath += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        target = FindClosestTarget(targetTag, defaultTargetTransform);
        if (currentMode == Mode.Idle)
            OnIdle();
        if (currentMode == Mode.Moveing)
            OnMovement();
        if (currentMode == Mode.Attacking)
            OnAttacking();
    }

    protected void OnAttacking()
    {
        if (!MovementIndicator())
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
    protected abstract void OnMovement();
    protected void OnIdle()
    {
        if (MovementIndicator())
            currentMode = Mode.Moveing;
        else
            currentMode = Mode.Attacking;
    }
    protected abstract void DealDamage();
    protected abstract bool MovementIndicator();
    protected Transform FindClosestTarget(string targetTag, Transform defaultTransform)
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, SightRange, transform.forward, SightRange);

        if (hits.Length < 1)
            return defaultTransform;

        Transform closest = defaultTransform;
        float closestRange = -1;
        foreach (RaycastHit hit in hits)
        {
            print(hit.transform.gameObject);
            if (closestRange < 0)
            {
                if (hit.transform.tag == targetTag)
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
                    if (hit.transform.tag == targetTag)
                    {
                        closest = hit.transform;
                        closestRange = hitRange;
                    }
                }
            }
        }
        return closest;
    }

    public void OnDeath()
    {
        if (currentMode == Mode.Dead)
            return;
        agent.destination = transform.position;
        currentMode = Mode.Dead;
        gameObject.tag = "Dead";
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
