using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerHandler : MonoBehaviour
{
    private EVRButtonId triggerButton = EVRButtonId.k_EButton_SteamVR_Trigger;
    private EVRButtonId dLeft = EVRButtonId.k_EButton_DPad_Left;
    private EVRButtonId dRight = EVRButtonId.k_EButton_DPad_Right;
    private EVRButtonId grip = EVRButtonId.k_EButton_Grip;

    #region Properties

    private bool _triggerButtonDown = false;
    public bool TriggerButtonDown
    {
        get { return _triggerButtonDown; }
        private set { _triggerButtonDown = value; }
    }

    private bool _dLeftDown = false;
    public bool DLeftDown
    {
        get { return _dLeftDown; }
        set { _dLeftDown = value; }
    }

    private bool _dRightDown = false;
    public bool DRightDown
    {
        get { return _dRightDown; }
        set { _dRightDown = value; }
    }

    private bool _gripped = false;
    public bool GripButtonDown
    {
        get { return _gripped; }
        set { _gripped = value; }
    }

    private SteamVR_TrackedObject _trackedObject;
    private SteamVR_Controller.Device Controller
    {
        get
        {
            if (_trackedObject == null)
                _trackedObject = GetComponent<SteamVR_TrackedObject>();

            return SteamVR_Controller.Input((int)_trackedObject.index);
        }
    }

    private Vector2 circlePadValue;
    public Vector2 CirclePadValue
    {
        get { return circlePadValue; }
        private set { circlePadValue = value; }
    }

    private bool canTrigger;
    public bool CanTrigger
    {
        get { return canTrigger; }
        set { canTrigger = value; }
    }

    #endregion

    [SerializeField]
    private float _triggerInputDelay = .5f;

    void Update()
    {
        _triggerInputDelay -= Time.deltaTime;
         CanTrigger = _triggerInputDelay <= 0;
        
        if (Controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        TriggerButtonDown = Controller.GetPressDown(triggerButton);
        DLeftDown = Controller.GetPress(dLeft);
        DRightDown = Controller.GetPress(dRight);
        GripButtonDown = Controller.GetPressDown(grip);
        CirclePadValue = Controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}

