using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float panSpeed;
    [SerializeField]
    private float panPadding;
    [SerializeField]
    private float scrollSpeed;

    // Scrolling velocity
    private Vector3 velocity;

    // Track scrolling
    private float newPositionY;

    private Transform pivot;
    private float initialpivotY;

    private void Awake()
    {
        velocity = Vector3.zero;
        newPositionY = transform.position.y;
        pivot = GameObject.Find("Pivot").GetComponent<Transform>();
        initialpivotY = pivot.position.y;
    }

    private void Update()
    {
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panPadding)
        {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panPadding)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panPadding)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panPadding)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Smooth scrolling
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed * 1000f * Time.deltaTime;
        newPositionY -= scroll;
        newPositionY = Mathf.Clamp(newPositionY, 5f, 20f);
        Vector3 newPosition = new Vector3(transform.position.x, newPositionY, transform.position.z);
        newPosition.y = Mathf.Clamp(newPosition.y, 5f, 20f);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, 0.5f);

        // Rotate camera after scrolling
        Vector3 direction = new Vector3(pivot.position.x, initialpivotY, pivot.position.z) - newPosition;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, scrollSpeed / 5f * Time.deltaTime);
    }
}
