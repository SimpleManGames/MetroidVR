using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildIgnoreParentRotation : MonoBehaviour
{
    public Transform parentTranform;

    public Vector3 offSet;

    private void Update()
    {
        transform.position = parentTranform.position + offSet;
    }

}