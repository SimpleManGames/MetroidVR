using UnityEngine;

[ExecuteInEditMode]
public class CustomIk : MonoBehaviour
{
    public Transform target;
    public Transform handTransform;

    public Transform shoulderTransform;
    public Transform shoulderElbowPoint;
    public float shoulderLength;

    public Transform wristTransform;
    public Transform wristElbowPoint;
    public float wristLength;

    public Transform elbowWeight;

    public float elbowZ;
    public float distToTarget;

    void Awake()
    {
        shoulderLength = Vector3.Distance(shoulderTransform.position, shoulderElbowPoint.position);
        wristLength = Vector3.Distance(wristTransform.position, wristElbowPoint.position);
    }

    void Update()
    {
        handTransform.rotation = target.rotation;
        handTransform.position = wristTransform.position;

        transform.LookAt(target, transform.position - elbowWeight.position);
        distToTarget = Vector3.Distance(target.position, shoulderTransform.position);
        elbowZ = (Mathf.Pow(distToTarget, 2) - Mathf.Pow(wristLength, 2) + Mathf.Pow(shoulderLength, 2)) / (distToTarget * 2);

        Vector3 localPosition = wristTransform.localPosition;
        localPosition.z = Mathf.Clamp(Vector3.Distance(shoulderTransform.position, target.position), 0, wristLength + shoulderLength);
        wristTransform.localPosition = localPosition;

        if (distToTarget < shoulderLength + wristLength && distToTarget > Mathf.Max(shoulderLength, wristLength) - Mathf.Min(shoulderLength, wristLength))
        {
            shoulderTransform.localRotation = Quaternion.Euler(Mathf.Acos(elbowZ / shoulderLength) * Mathf.Rad2Deg, 0, 0);
            wristTransform.localRotation = Quaternion.Euler(-(Mathf.Acos((distToTarget - elbowZ) / wristLength) * Mathf.Rad2Deg), 0, 0);
        }

        if (distToTarget >= shoulderLength + wristLength)
        {
            shoulderTransform.localRotation = Quaternion.Euler(0, 0, 0);
            wristTransform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (distToTarget <= Mathf.Max(shoulderLength, wristLength) - Mathf.Min(shoulderLength, wristLength))
        {
            shoulderTransform.localRotation = Quaternion.Euler(0, 0, 0);
            wristTransform.localRotation = Quaternion.Euler(180, 0, 0);
        }
    }
}
