using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPositionAtFeet : MonoBehaviour {

    public Transform trans;

    public void Update()
    {
        this.transform.position = new Vector3(trans.position.x, 0, trans.position.z);
    }
}
