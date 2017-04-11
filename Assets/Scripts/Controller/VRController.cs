using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour
{
    [SerializeField]
    private Transform RoomScaleTransform;

    [SerializeField]
    private VRCameraRaycastDown CameraRaycaster;

    [SerializeField]
    private Transform CameraPosition;

    [SerializeField]
    private ControllerHandler LeftHand;

    [SerializeField]
    private ControllerHandler RightHand;

    [SerializeField]
    private Transform leanPositionTracker;

    private Vector3 DirectionRotationAxis;

    public MovementSettings movementSettings;
    
    private void Update()
    {
        switch (movementSettings.movementMethod)
        {
            case MovementSettings.MovementMethod.CameraLook:
                DirectionRotationAxis = new Vector3(CameraPosition.transform.forward.x, 0, CameraPosition.transform.forward.z);
                break;
            case MovementSettings.MovementMethod.LeftHandPoint:
                DirectionRotationAxis = new Vector3(LeftHand.transform.forward.x, 0, LeftHand.transform.forward.z);
                break;
            case MovementSettings.MovementMethod.LeftDPadSteering:
                DirectionRotationAxis = new Vector3(-LeftHand.CirclePadValue.y, 0, LeftHand.CirclePadValue.x);
                break;
            case MovementSettings.MovementMethod.LeanSteering:
                if (!LeftHand.TriggerButtonHeld)
                    leanPositionTracker.localPosition = CameraPosition.localPosition;

                DirectionRotationAxis = new Vector3(CameraPosition.localPosition.x - leanPositionTracker.localPosition.x, 0, CameraPosition.localPosition.z - leanPositionTracker.localPosition.z);
                Boost();
                break;
        }

        if (LeftHand.CirclePadValue != Vector2.zero && movementSettings.movementMethod != MovementSettings.MovementMethod.LeanSteering)
            Boost();
    }

    private void Boost()
    {
        RoomScaleTransform.position = Vector3.Slerp(RoomScaleTransform.position, RoomScaleTransform.position + DirectionRotationAxis, 1f * movementSettings.BoostSpeed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(LeftHand.transform.position, LeftHand.transform.position + DirectionRotationAxis);

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(leanPositionTracker.position, .2f); 
    }

    [System.Serializable]
    public struct MovementSettings
    {
        public enum MovementMethod { CameraLook, LeftHandPoint, LeftDPadSteering, LeanSteering }
        public MovementMethod movementMethod;
        public float BoostSpeed;
    }
}