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

    NavMeshAgent agent;
    private Transform target;
    Mode currentMode;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMode == Mode.Idle)
            OnIdle();
        else if (currentMode == Mode.Moveing)
            OnMovement();
        else if (currentMode == Mode.Attacking)
            OnAttacking();

        agent.destination = target.position;
    }

    private void OnIdle()
    {
        target = FindClosestTarget();
        animator.SetBool("IsMoving", true);
    }

    private Transform FindClosestTarget()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, SightRange, transform.forward);

        if (hits.Length < 1)
            return MainBase.Instance.transform;

        Transform closest = hits[0].transform;
        float closestRange = Vector3.Distance(closest.position, transform.position);
        foreach (RaycastHit hit in hits)
        {
            float hitRange = Vector3.Distance(hit.transform.position, transform.position);
            if (hitRange < closestRange)
            {
                closest = hit.transform;
                closestRange = hitRange;
            }
        }
        return closest;

    }

    private void OnMovement()
    {
        throw new NotImplementedException();
    }

    private void OnAttacking()
    {
        throw new NotImplementedException();
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
