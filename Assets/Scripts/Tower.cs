using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Tower : MonoBehaviour
{
    public Transform currentEnemy;

    [Header("Tower Setup")]
    [SerializeField] private Transform towerHead;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask whatIsEnemy;

    private void Update()
    {
        if (!currentEnemy)
        {
            currentEnemy = FindRandomEnemyWithinRange();
            return;
        }

        if (Vector3.Distance(transform.position, currentEnemy.position) > attackRange)
        {
            currentEnemy = null;
        }

        RotateTowardsEnemy();
    }

    private Transform FindRandomEnemyWithinRange()
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

    private void RotateTowardsEnemy()
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
