using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInfo : MonoBehaviour
{
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


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(position, rectSize);
    }
}
