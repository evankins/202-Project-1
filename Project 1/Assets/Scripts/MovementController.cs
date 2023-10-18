using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    Vector3 objectPosition = Vector3.zero;

    [SerializeField]
    float speed = 1f;

    Vector3 direction = Vector3.zero;

    Vector3 velocity = Vector3.zero;

    // camera (bounds) values
    Camera cameraObject;
    float totalCamHeight;
    float totalCamWidth;


    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;

        cameraObject = Camera.main;
        totalCamHeight = cameraObject.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * cameraObject.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = direction * speed * Time.deltaTime;

        objectPosition += velocity;

        // === Check for OB ===

        // screen width OB
        if (objectPosition.x > (totalCamWidth / 2))
        {
            objectPosition.x = totalCamWidth / 2;
        }
        if (objectPosition.x < (-totalCamWidth / 2))
        {
            objectPosition.x = -totalCamWidth / 2;
        }
        // screen height OB
        if (objectPosition.y > (totalCamHeight / 2))
        {
            objectPosition.y = totalCamHeight / 2;
        }
        if (objectPosition.y < (-totalCamHeight / 2))
        {
            objectPosition.y = -totalCamHeight / 2;
        }

        transform.position = objectPosition;
    }

    public void SetDirection(Vector3 newDirection)
    {
        if (newDirection != null)
        {
            direction = newDirection.normalized;
        }
    }
}
