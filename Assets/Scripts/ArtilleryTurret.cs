using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryTurret : Turret
{
    [Header("Unity Specifications")]
    [SerializeField]
    private GameObject bullet;

    private void Awake()
    {
        fireCountdown = fireCooldown;
    }

    private void Start()
    {
        StartCoroutine(SearchTarget());
    }

    private void Update()
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

        // Only shoot when not on cooldown or on rotation
        if (fireCountdown >= fireCooldown && RotationDone(direction))
        {
            fireCountdown = 0f;
            Fire();
        }
        fireCountdown += Time.deltaTime;
    }

    protected override void Fire()
    {
        ParticleSystem particle = Instantiate(fireEffect, firePoint.transform.position, firePoint.transform.rotation, firePoint.transform).GetComponent<ParticleSystem>();
        Destroy(particle.gameObject, particle.main.duration);
        Bullet currentBullet = Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation).GetComponent<Bullet>();
        currentBullet.target = target;
    }

    protected override void RotateTurret(Vector3 direction)
    {
        direction.y = 0f;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        partToRotate.transform.rotation = Quaternion.LerpUnclamped(partToRotate.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private bool RotationDone(Vector3 direction)
    {
        return Quaternion.Angle(partToRotate.transform.rotation, Quaternion.LookRotation(direction)) <= 10f;
    }

    protected override void ElevateGuns(Vector3 direction)
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
}
