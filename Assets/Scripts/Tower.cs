using System;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public Transform currentEnemyTransform;

    [Header("Tower Setup")]
    [SerializeField] private Transform towerHead;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        RotateTowardsEnemy();
    }

    private void RotateTowardsEnemy()
    {
        Vector3 directionToEnemy = currentEnemyTransform.position - towerHead.position;

        Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);

        Vector3 rotation =
            Quaternion.Lerp(towerHead.rotation, lookRotation, rotationSpeed * Time.deltaTime).eulerAngles;

        towerHead.rotation = Quaternion.Euler(rotation);
    }
}
