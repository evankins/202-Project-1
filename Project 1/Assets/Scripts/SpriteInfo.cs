using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
    [SerializeField]
    float radius = 1f;

    public float Radius
    {
        get { return radius; }
    }

    public Vector3 position
    {
        get { return transform.position; }
    }

    [SerializeField]
    Vector2 rectSize = Vector2.one;

    // Properties for Min and Max
    public Vector2 RectMin
    {
        get
        {
            Vector2 rectMin;
            rectMin.x = -(rectSize.x / 2);
            rectMin.y = -(rectSize.y / 2);
            return rectMin;
        }
    }

    public Vector2 RectMax
    {
        get { return rectSize; }
    }



    bool isColliding = false;

    public bool IsColliding
    {
        set { isColliding = value; }
    }

    [SerializeField]
    SpriteRenderer renderer; // don't need this in project one

    // Update is called once per frame
    void Update()
    {
        // don't need this for project one
        if (isColliding)
        {
            renderer.color = Color.red;
        }
        else
        {
            renderer.color = Color.white;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Gizmos.DrawWireSphere(position, radius);
        Gizmos.DrawWireCube(position, rectSize);
    }
}
