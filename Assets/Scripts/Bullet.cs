using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    float speed;
    [SerializeField]
    GameObject explosionHitBox;

    Vector3 direction;
    Vector3 initialPosition;
    Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
        targetPosition = target.position;
        direction = targetPosition - transform.position;
        direction.y = 0.5f; // Offset
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        Vector3 currentDirection = transform.position - initialPosition;

        // Prevent flying passes target
        if (currentDirection.magnitude >= direction.magnitude)
        {
            TargetHit();
            return;
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void TargetHit()
    {
        Instantiate(explosionHitBox, targetPosition, explosionHitBox.transform.rotation); 
        Destroy(gameObject);
    }
}
