using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoostTrigger : MonoBehaviour
{
    private CollisionInfo collisionInfo;

    [SerializeField]
    private Transform _cameraTransform;
    public Transform CameraTransform
    {
        get
        {
            if (_cameraTransform == null)
                _cameraTransform = GameObject.Find("Camera (head)").transform;

            return _cameraTransform;
        }
    }
    private Vector3 CameraFace2DAxis;

    public Transform CameraRigTransform;
    public Rigidbody CameraRigRigibody;
    public Transform CameraPivot;

    public Transform RayCastDirectionPosition;

    [SerializeField]
    private ControllerHandler controller;

    SteamVR_Controller.Device device;

    private float boostAmount = 25;

    bool Boost;
    Vector3 lerpToPosition;

    public int rayCountWidth = 4;
    public int rayCountHeight = 4;

    public float maxClimbAngle = 45f;

    [Range(0, 1)]
    public float decendRate = .1f;

    private Vector3 lastPosition;

    private Vector3 StartingOffset;

    public float skinWidth = .25f;

    Vector3 moveVec;

    private void Awake()
    {
        StartingOffset = transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(CameraTransform.position.x, CameraTransform.position.y + StartingOffset.y, CameraTransform.position.z);
        CameraFace2DAxis = new Vector3(CameraTransform.forward.x, 0, CameraTransform.forward.z);
        Debug.DrawLine(CameraTransform.position, CameraTransform.position + CameraFace2DAxis);

        if (controller.CirclePadValue != Vector2.zero)
            Boost = true;
        else
            Boost = false;

        if (Boost)
            lerpToPosition = CameraTransform.position + CameraFace2DAxis;

        lastPosition = CameraRigRigibody.position;
    }

    private void FixedUpdate()
    {
        if (Boost)
            CameraRigRigibody.AddRelativeForce((lerpToPosition - CameraTransform.position) * boostAmount, ForceMode.Acceleration);

        HorizontalCollisionCheck();
        VerticalCollisionCheck();
    }

    void HorizontalCollisionCheck()
    {
        if (controller.CirclePadValue != Vector2.zero)
            moveVec = new Vector3(Mathf.RoundToInt(CameraRigRigibody.velocity.x), 0, Mathf.RoundToInt(CameraRigRigibody.velocity.z)).normalized;

        for (int y = 0; y < rayCountHeight + 1; y++)
        {
            Collider collider = GetComponentInChildren<Collider>();

            Vector3 rayPosition = RayCastDirectionPosition.position;
            rayPosition.y += y * collider.bounds.max.y / rayCountHeight;
            Ray ray = new Ray(rayPosition, moveVec);

            Debug.DrawRay(ray.origin, ray.direction);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, .5f))
            {
                collisionInfo.climbingSlope = true;
                collisionInfo.slopeAngle = Vector3.Angle(hit.normal, Vector2.up);

                if (collisionInfo.climbingSlope)
                {
                    ClimbSlope(y);
                }
            }
            else
            {
                collisionInfo.Reset();
            }
        }
    }

    void ClimbSlope(int rayIndex)
    {
        if (rayIndex == 0 && collisionInfo.slopeAngle <= maxClimbAngle)
        {
            float moveDistance = Mathf.Abs(CameraRigRigibody.velocity.x + CameraRigRigibody.velocity.z);
            float climbVelocityY = Mathf.Sin(collisionInfo.slopeAngle * Mathf.Deg2Rad) * moveDistance;

            if (CameraRigRigibody.velocity.y < climbVelocityY)
            {
                CameraRigRigibody.velocity = new Vector3(CameraRigRigibody.velocity.x, climbVelocityY, CameraRigRigibody.velocity.z);
            }
        }
    }

    Vector3 debugposition;

    void VerticalCollisionCheck()
    {
        Vector3 rayCastPosition = new Vector3(CameraTransform.position.x, CameraTransform.position.y - skinWidth, CameraTransform.position.z);
        Ray ray = new Ray(rayCastPosition, Vector3.down);

        Debug.DrawRay(ray.origin, ray.direction * 100);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            float hitDist = rayCastPosition.y - hit.point.y;
            if (hitDist > 0f /*&& !collisionInfo.climbingSlope*/)
            {
                var mesh = CameraRigTransform.GetComponent<MeshFilter>().mesh;

                var xVerts = mesh.vertices.Select(x => x.x);
                var yVerts = mesh.vertices.Select(x => x.y);
                var zVerts = mesh.vertices.Select(x => x.z);
                var ave = new Vector3(xVerts.Average(), yVerts.Average(), zVerts.Average());

                debugposition = CameraRigTransform.position + ave;

                CameraPivot.position = new Vector3(CameraPivot.position.x, hit.point.y, CameraPivot.position.z);
                RayCastDirectionPosition.position = new Vector3(RayCastDirectionPosition.position.x, CameraPivot.position.y, RayCastDirectionPosition.position.z);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawSphere(lerpToPosition, .1f);
        Gizmos.DrawLine(CameraTransform.position, lerpToPosition);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(debugposition, .1f);
    }

    public struct CollisionInfo
    {
        public bool climbingSlope;
        public bool descendingSlope;
        public float slopeAngle;

        public void Reset()
        {
            climbingSlope = false;
            descendingSlope = false;

            slopeAngle = 0;
        }
    }
}
