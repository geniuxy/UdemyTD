using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_CrossBow : Tower
{
    [Header("CrossBow Details")] 
    [SerializeField] private Transform gunPoint;

    protected override void Attack()
    {
        Vector3 directionToEnemy = DirectionToEnemy(gunPoint);

        if (Physics.Raycast(gunPoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity))
        {
            towerHead.forward = directionToEnemy;
            
            Debug.Log(hitInfo.collider.gameObject.name + " was attacked!");
            Debug.DrawLine(gunPoint.position, hitInfo.point);
        }
    }
}