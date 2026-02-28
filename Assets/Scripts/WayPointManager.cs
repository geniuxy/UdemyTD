using System;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    [SerializeField] private Transform[] wayPoints;

    public Transform[] GetWayPoints() => wayPoints;

    private void Start()
    {
        wayPoints = new Transform[transform.childCount];
        
        for (int i = 0; i < transform.childCount; i++)
        {
            wayPoints[i] = transform.GetChild(i);
        }
    }
}
