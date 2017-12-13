using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour {
    public GameObject target;
    public GameObject[] allenemies;
    public float range = 1f;

    void Start() {
    }

    private void Update() {
        Vector3 relativePos = target.transform.position - transform.position;
        Vector3 LookRot = Quaternion.LookRotation(relativePos).eulerAngles;
        transform.rotation = Quaternion.Euler(-90, LookRot.y, transform.position.z);

        allenemies = GameObject.FindGameObjectsWithTag("Enemy");
        FindClosest();
    }

    public GameObject FindClosest() {
        if (allenemies.Length == 0) return null;

        GameObject theclosest = allenemies[0];

        float DistToCurrent = Vector3.Distance(transform.position, theclosest.transform.position);
        float DistToi = Vector3.Distance(transform.position, theclosest.transform.position);

        foreach (GameObject i in allenemies) {
            if (DistToi < DistToCurrent) {
                theclosest = i;
            }
        }

        return theclosest;
    }
}