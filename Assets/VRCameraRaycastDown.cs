using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRCameraRaycastDown : MonoBehaviour
{
    private Vector3 hitPoint;
    public Vector3 GetHitPoint
    {
        get { return hitPoint; }
        private set
        {
            hitPoint = value;
        }
    }

    public float skinWidth = .25f;

    private float HeightOfPlayer
    {
        get
        {
            return transform.position.y;
        }
    }

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, HeightOfPlayer + (skinWidth * 2)))
            GetHitPoint = hit.point;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GetHitPoint, .1f);

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -HeightOfPlayer, 0));
    }
}
