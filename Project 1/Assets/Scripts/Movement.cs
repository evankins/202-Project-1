using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 objectPosition;

    [SerializeField]
    float speed = 1f;

    Vector3 direction = Vector3.right;

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
        if (objectPosition.x > ((totalCamWidth / 2) * 1.2))
        {
            SpawnManager.Instance.DestroyObject(this.gameObject);
        }
        if (objectPosition.x < ((-totalCamWidth / 2) * 1.2))
        {
            SpawnManager.Instance.DestroyObject(this.gameObject);
        }
        // screen height OB
        if (objectPosition.y > ((totalCamHeight / 2) * 1.2))
        {
            SpawnManager.Instance.DestroyObject(this.gameObject);
        }
        if (objectPosition.y < ((-totalCamHeight / 2) * 1.2))
        {
            SpawnManager.Instance.DestroyObject(this.gameObject);
        }

        transform.position = objectPosition;
    }

}
