using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperTurret : MonoBehaviour {
    public Transform target1;

    void Start() {
        target1 = transform.parent.gameObject.GetComponent<TurretScript>().target.transform;
    }

    void Update() {
        Vector3 relativePos = target1.position - transform.position;
        Vector3 LookRot = Quaternion.LookRotation(relativePos).eulerAngles;
        transform.localRotation = Quaternion.Euler(LookRot.x-180, 0, 0);
    }
}