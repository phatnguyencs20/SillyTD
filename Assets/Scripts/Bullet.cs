using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform target;

    [SerializeField]
    float speed;

    Vector3 direction;
    Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
        direction = target.position - transform.position;
        direction.y = 0.5f;
        StartCoroutine(SelfDestruct());
    }

    void Update()
    {
        Vector3 currentDirection = transform.position - initialPosition;

        if (currentDirection.magnitude >= direction.magnitude)
        {
            TargetHit();
            return;
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }

    void TargetHit()
    {
        Debug.Log("Hit!!");
        Destroy(gameObject);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
