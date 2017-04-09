using UnityEngine;

public class VRShoulder : MonoBehaviour
{
    public Transform CameraTransform;

    public Transform LeftHand;

    public Transform RightHand;

    private Vector3 averageHandPosition;
    private Vector3 cameraForwardPosition;
    private Vector3 handCameraAverage;

    void Update()
    {
        averageHandPosition = (LeftHand.position + RightHand.position) / 2;
        averageHandPosition.y = 0f;

        cameraForwardPosition = CameraTransform.position + CameraTransform.forward;
        cameraForwardPosition.y = 0f;

        handCameraAverage = (averageHandPosition + cameraForwardPosition) / 2;
        handCameraAverage.y = transform.position.y;

        transform.LookAt(handCameraAverage);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(averageHandPosition, .1f);

        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(cameraForwardPosition, .1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(handCameraAverage, .1f);
    }
}
