using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    [SerializeField]
    float speed = 5f;

    Transform waypointToMove;
    int nextWayPointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypointToMove = WayPoints.wayPoints[nextWayPointIndex];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToMove = waypointToMove.position - transform.position;
        transform.Translate(directionToMove.normalized * speed * Time.deltaTime);

        if(Vector3.Distance(waypointToMove.position, transform.position) <= 0.1f)
        {
            if(nextWayPointIndex < WayPoints.wayPoints.Length - 1)
            {
                nextWayPointIndex++;
                waypointToMove = GetNextWayPoint(nextWayPointIndex);
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
