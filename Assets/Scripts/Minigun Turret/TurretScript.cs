using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {
    public GameObject target;
    public List<GameObject> inrange;
    public float range = 1f;

    private void Update() {
        Vector3 relativePos = target.transform.position - transform.position;
        Vector3 LookRot = Quaternion.LookRotation(relativePos).eulerAngles;
        transform.rotation = Quaternion.Euler(-90, LookRot.y, transform.position.z);
    }

    public void FindLast() {

    }
}