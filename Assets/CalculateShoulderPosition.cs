using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateShoulderPosition : MonoBehaviour
{

    public Transform LeftHandPosition;
    public Transform RightHandPosition;

    public Transform HeadPosition;

    private Vector3 averageHandPosition;

    private void Update()
    {
        Vector3 cameraForward = (HeadPosition.transform.GetComponent<Camera>().transform.forward);
        cameraForward.y = 0.0f;
        var q1 = Quaternion.LookRotation(cameraForward, Vector3.up);

        averageHandPosition = (LeftHandPosition.position + RightHandPosition.position) / 2;
        averageHandPosition.y = 0;

        
        Quaternion angle = Quaternion.LookRotation
                           (Vector3.Normalize(new Vector3(HeadPosition.position.x, 0, HeadPosition.position.z) - averageHandPosition), Vector3.up);

        //var q2 = Quaternion.AngleAxis(angle, Vector3.up);
        transform.localRotation = Quaternion.Slerp(q1, angle, 0.5f);//q2 * q1;

        /*Debug.Log("Angle returns: " + angle + '\n' 
                + "Q2 returns: " + q2.eulerAngles + '\n' 
                + "Q1 returns: " + q1.eulerAngles + '\n'
                + "Average returns: " + averageHandPosition);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(averageHandPosition, .1f);
    }
}
