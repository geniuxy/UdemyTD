using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_CrossBow : Tower
{
    private Visuals_CrossBow visuals;
    
    [Header("CrossBow Details")] 
    [SerializeField] private Transform gunPoint;

    protected override void Awake()
    {
        base.Awake();

        visuals = GetComponent<Visuals_CrossBow>();
    }

    protected override void Attack()
    {
        Vector3 directionToEnemy = DirectionToEnemy(gunPoint);

        if (Physics.Raycast(gunPoint.position, directionToEnemy, out RaycastHit hitInfo, Mathf.Infinity))
        {
            towerHead.forward = directionToEnemy;
            
            Debug.Log(hitInfo.collider.gameObject.name + " was attacked!");
            Debug.DrawLine(gunPoint.position, hitInfo.point);
            
            visuals.PlayAttackFX(gunPoint.position, hitInfo.point);
            visuals.PlayReloadFX(attackCoolDown);
        }
    }
}