using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHandler : MonoBehaviour
{
    public bool triggerButtonDown = false;
    public bool dLeftDown = false;
    public bool dRightDown = false;
    public bool gripped = false;

    private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
    private Valve.VR.EVRButtonId dLeft = Valve.VR.EVRButtonId.k_EButton_DPad_Left;
    private Valve.VR.EVRButtonId dRight = Valve.VR.EVRButtonId.k_EButton_DPad_Right;
    private Valve.VR.EVRButtonId grip = Valve.VR.EVRButtonId.k_EButton_Grip;

    float triggerBuffer = .5f;
    public bool canTrigger;

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public Vector2 CirclePadValue;

    void Start()
    {

        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {

        triggerBuffer -= Time.deltaTime;
        if (triggerBuffer <= 0)
        {
            canTrigger = true;
        }
        else { canTrigger = false; }
        //lr.SetPositions(transform.position, po.objToPlace.transform.position);
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }
        triggerButtonDown = controller.GetPressDown(triggerButton);
        dLeftDown = controller.GetPress(dLeft);
        dRightDown = controller.GetPress(dRight);
        gripped = controller.GetPressDown(grip);
        
        CirclePadValue = controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
        Debug.Log(CirclePadValue);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward);
    }
}

