using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTrigger : MonoBehaviour
{

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

    [SerializeField]
    private ControllerHandler controller;

    SteamVR_Controller.Device device;

    bool Boost;
    Vector3 lerpToPosition;

    void Update()
    {
        transform.position = new Vector3(CameraTransform.position.x, transform.position.y, CameraTransform.position.z);
        CameraFace2DAxis = new Vector3(CameraTransform.forward.x, 0, CameraTransform.forward.z);
        Debug.DrawLine(CameraTransform.position, CameraTransform.position + CameraFace2DAxis);

        if (controller.CirclePadValue != Vector2.zero)
            Boost = true;
        else
            Boost = false;

        Vector3 vel = new Vector3();
        if (Boost)
            lerpToPosition = CameraRigTransform.position + CameraFace2DAxis.normalized;

        CameraRigTransform.position = Vector3.SmoothDamp(CameraRigTransform.position, lerpToPosition, ref vel, .075f);
    }
}
