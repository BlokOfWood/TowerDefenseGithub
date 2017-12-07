using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {
    public GameObject target;

    private void Update() {
        Vector3 relativePos = target.transform.position - transform.position;
        Vector3 LookRot = Quaternion.LookRotation(relativePos).eulerAngles;
        transform.rotation = Quaternion.Euler(-90, LookRot.y+5, transform.position.z);
    }
}