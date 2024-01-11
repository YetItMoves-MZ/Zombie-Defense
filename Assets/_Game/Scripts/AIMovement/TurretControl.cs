using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    protected enum Mode
    {
        Idle,
        Attacking
    }

    public float AttackRange = 1f;
    public int Damage = 1;
    public float AttackSpeed = 1f;
    public Transform TurretObject;

    Transform target;
    Mode currentMode;
    Stats myStats;
    float timer;

    virtual protected void Start()
    {
        myStats = GetComponent<Stats>();
        currentMode = Mode.Idle;
        myStats.OnDeath += OnDeath;
    }

    void Update()
    {
        target = FindClosestTarget();
        if (currentMode == Mode.Idle)
            OnIdle();
        if (currentMode == Mode.Attacking)
            OnAttacking();
    }

    private void OnAttacking()
    {
        TurretObject.LookAt(target);
        if (timer <= 0)
        {
            DealDamage();
            timer = AttackSpeed;
        }
        timer -= Time.deltaTime;
    }

    private void DealDamage()
    {
        target.GetComponent<Stats>().Health -= Damage;
    }

    private void OnIdle()
    {
        if (target != null)
            currentMode = Mode.Attacking;
        else
            TurretObject.Rotate(0f, 10f * Time.deltaTime, 0f);
    }

    private Transform FindClosestTarget()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, AttackRange, transform.forward, AttackRange);

        if (hits.Length < 1)
            return null;

        Transform closest = null;
        float closestRange = -1;
        foreach (RaycastHit hit in hits)
        {
            print(hit.transform.gameObject);
            if (closestRange < 0)
            {
                if (hit.transform.tag == "Enemy")
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
                    if (hit.transform.tag == "Enemy")
                    {
                        closest = hit.transform;
                        closestRange = hitRange;
                    }
                }
            }
        }
        return closest;
    }

    void OnDeath()
    {
        Destroy(gameObject);
    }


}
