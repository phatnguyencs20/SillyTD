using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [Header("Specifications")]
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float rotationSpeed;

    int nextWayPointIndex;
    Vector3 directionToMove;
    Transform waypointToMove;
    
    Mob()
    {
        nextWayPointIndex = 0;
    }
    
    void Start()
    {
        waypointToMove = WayPoints.wayPoints[nextWayPointIndex];
        directionToMove = waypointToMove.position - transform.position;
        transform.rotation = Quaternion.LookRotation(directionToMove);
    }

    void FixedUpdate()
    {
        transform.Translate(directionToMove.normalized * movementSpeed * Time.deltaTime, Space.World);
        Quaternion lookRotation = Quaternion.LookRotation(directionToMove);
        transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        // If destination is reached, find next destination 
        if (Vector3.Distance(waypointToMove.position, transform.position) <= 0.1f)
        {
            if (nextWayPointIndex < WayPoints.wayPoints.Length - 1)
            {
                nextWayPointIndex++;
                waypointToMove = GetNextWayPoint(nextWayPointIndex);
                directionToMove = waypointToMove.position - transform.position;
            }
        }
    }

    Transform GetNextWayPoint(int nextWayPointIndex)
    {
        return WayPoints.wayPoints[nextWayPointIndex];
    }

    public float GetPathLengthFromEndPoint()
    {
        float pathLength = 0f;
        int tempNextWayPoint = nextWayPointIndex;
        Transform pointA = transform;
        Transform pointB = WayPoints.wayPoints[tempNextWayPoint];

        while (tempNextWayPoint < WayPoints.wayPoints.Length)
        {
            pathLength += Vector3.Distance(pointA.position, pointB.position);
            tempNextWayPoint++;

            if (tempNextWayPoint < WayPoints.wayPoints.Length)
            {
                pointA = pointB;
                pointB = WayPoints.wayPoints[tempNextWayPoint];
            }
        }

        return pathLength;
    }
}
