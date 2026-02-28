using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent agent;

    [SerializeField] private float turnSpeed = 10.0f;
    [SerializeField] private Transform[] wayPoints;
    private int wayPointIndex;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.avoidancePriority = Mathf.RoundToInt(agent.speed * 10.0f);
    }

    private void Start()
    {
        wayPoints = FindFirstObjectByType<WayPointManager>().GetWayPoints();
    }

    private void Update()
    {
        FaceTarget(agent.steeringTarget);
        if (agent.remainingDistance < 0.5f)
        {
            agent.SetDestination(GetNextWayPoint());
        }
    }

    private void FaceTarget(Vector3 newTarget)
    {
        Vector3 directionToTarget = newTarget - transform.position;
        if (directionToTarget.magnitude == 0)
        {
            return;
        }

        directionToTarget.y = 0;

        Quaternion newRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }

    // Start is called before the first frame update
    private Vector3 GetNextWayPoint()
    {
        if (wayPointIndex >= wayPoints.Length)
        {
            wayPointIndex = 0;
            // return transform.position;
        }

        Vector3 targetPoint = wayPoints[wayPointIndex++].position;

        return targetPoint;
    }
}