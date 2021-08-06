using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField]
    float range = 10f;

    [SerializeField]
    float searchInterval = 0.25f;

    [SerializeField]
    GameObject partToRotate;

    [SerializeField]
    GameObject partToElevate;

    [SerializeField]
    float rotationSpeed = 5f;

    [SerializeField]
    float elevationSpeed = 2f;

    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SearchTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 direction = target.position - transform.position;
        RotateTurret(direction);
        ElevateGuns(direction);
    }

    void RotateTurret(Vector3 direction)
    {
        direction.y = 0;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        partToRotate.transform.rotation = Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void ElevateGuns(Vector3 direction)
    {
        // Calculate angle by cosine function
        float targetDistance = direction.magnitude;
        direction.y = 0;
        float targetRange = direction.magnitude;
        float angle = -Mathf.Acos(targetRange / targetDistance) * 180 / Mathf.PI;
        angle = Mathf.Clamp(angle, -30f, 0f);

        Quaternion lookRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        partToElevate.transform.localRotation = Quaternion.Lerp(partToElevate.transform.localRotation, lookRotation, elevationSpeed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator SearchTarget()
    {
        while (true)
        {
            Mob[] mobs = FindObjectsOfType<Mob>();
            target = GetClosestTargetFromEndPoint(mobs);
            yield return new WaitForSeconds(searchInterval);
        }
    }

    IEnumerator LockTarget()
    {
        while (target != null && Vector3.Distance(transform.position, target.position) <= range)
        {
            yield return new WaitForSeconds(searchInterval);
        }
    }

    Transform GetClosestTarget(Mob[] mobs)
    {
        Transform closestTarget = null;
        float closestDistance = float.PositiveInfinity;
        foreach (var mob in mobs)
        {
            float targetDistance = Vector3.Distance(transform.position, mob.transform.position);
            if (targetDistance <= range && targetDistance < closestDistance)
            {
                closestTarget = mob.transform;
                closestDistance = targetDistance;
            }
        }

        return closestTarget;
    }

    Transform GetClosestTargetFromEndPoint(Mob[] mobs)
    {
        Transform closestTargetFromEndPoint = null;
        float closestDistanceFromEndPoint = float.PositiveInfinity;
        foreach (var mob in mobs)
        {
            float targetDistance = Vector3.Distance(transform.position, mob.transform.position);
            float targetDistanceFromEndPoint = mob.GetPathLengthFromEndPoint();
            if (targetDistance <= range && targetDistanceFromEndPoint < closestDistanceFromEndPoint)
            {
                closestTargetFromEndPoint = mob.transform;
                closestDistanceFromEndPoint = targetDistanceFromEndPoint;
            }
        }

        return closestTargetFromEndPoint;
    }
}
