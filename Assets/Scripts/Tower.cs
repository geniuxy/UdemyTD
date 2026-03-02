using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    public Transform currentEnemy;

    [Header("Tower Setup")]
    [SerializeField] protected Transform towerHead;
    [SerializeField] protected float rotationSpeed = 60.0f;

    [Header("Attack Factors")]
    [SerializeField] protected float attackCoolDown = 1.5f;
    protected float lastAttackTime;
    [SerializeField] protected float attackRange = 1.5f;
    [SerializeField] protected LayerMask whatIsEnemy;

    protected virtual void Update()
    {
        if (!currentEnemy)
        {
            currentEnemy = FindRandomEnemyWithinRange();
            return;
        }

        if (CanAttack())
        {
            Attack();
        }

        if (Vector3.Distance(transform.position, currentEnemy.position) > attackRange)
        {
            currentEnemy = null;
        }

        RotateTowardsEnemy();
    }

    protected virtual void Attack()
    {
        Debug.Log("Attack performed at " + Time.time);
    }

    protected bool CanAttack()
    {
        if (!currentEnemy) return false;
        
        if (Time.time > lastAttackTime + attackCoolDown)
        {
            lastAttackTime = Time.time;
            return true;
        }

        return false;
    }

    protected virtual Transform FindRandomEnemyWithinRange()
    {
        List<Transform> possibleTargets = new List<Transform>();
        Collider[] enemiesAround = Physics.OverlapSphere(transform.position, attackRange, whatIsEnemy);

        foreach (Collider enemy in enemiesAround)
        {
            possibleTargets.Add(enemy.transform);
        }

        if (possibleTargets.Count <= 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, possibleTargets.Count);

        return possibleTargets[randomIndex];
    }

    protected Vector3 DirectionToEnemy(Transform startPoint)
    {
        return (currentEnemy.position - startPoint.position).normalized;
    }

    protected virtual void RotateTowardsEnemy()
    {
        if (!currentEnemy)
        {
            return;
        }

        Vector3 directionToEnemy = currentEnemy.position - towerHead.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        Vector3 rotation =
            Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

        towerHead.rotation = Quaternion.Euler(rotation);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
