using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Specifications")]
    [SerializeField]
    float fireCooldown;
    [SerializeField]
    float range;
    [SerializeField]
    float searchInterval;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float elevationSpeed;
    
    [Header("Unity Objects")]
    [SerializeField]
    GameObject partToRotate;
    [SerializeField]
    GameObject partToElevate;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject firePoint;
    public Transform target;

    float fireCountdown;
    
    // Start is called before the first frame update
    void Start()
    {
        fireCountdown = fireCooldown;
        StartCoroutine(SearchTarget());
    }

    //Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (fireCountdown <= fireCooldown)
            {
                fireCountdown += Time.deltaTime;
            }
   
            return;
        }

        Vector3 direction = target.position - transform.position;
        RotateTurret(direction);
        ElevateGuns(direction);

        if (fireCountdown >= fireCooldown)
        {
            fireCountdown = 0f;
            Shoot();
        }
        fireCountdown += Time.deltaTime;
    }

    void Shoot()
    {
        GameObject currentBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
        currentBullet.GetComponent<Bullet>().target = target;
    }

    void RotateTurret(Vector3 direction)
    {
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        partToRotate.transform.rotation = Quaternion.LerpUnclamped(partToRotate.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void ElevateGuns(Vector3 direction)
    {
        // Calculate angle by cosine function
        float targetDistance = direction.magnitude;
        direction.y = 0f;
        float targetRange = direction.magnitude;
        float angle = -Mathf.Acos(targetRange / targetDistance) * 180f / Mathf.PI;
        angle = Mathf.Clamp(angle, -30f, 0f);

        Quaternion lookRotation = Quaternion.Euler(new Vector3(angle, 0f, 0f));
        partToElevate.transform.localRotation = Quaternion.LerpUnclamped(partToElevate.transform.localRotation, lookRotation, elevationSpeed * Time.deltaTime);
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator SearchTarget()
    {
        while (true)
        {
            if (!TargetLocked())
            {
                Mob[] mobs = FindObjectsOfType<Mob>();
                target = GetClosestTargetFromEndPoint(mobs);
            }
            yield return new WaitForSeconds(searchInterval);
        }
    }

    bool TargetLocked()
    {
        return (target != null && Vector3.Distance(transform.position, target.position) <= range);
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
