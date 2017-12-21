using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunTurret : MonoBehaviour {
    [Header("Enemy Targeting")]
    public string enemytag = "Enemy";
    public GameObject[] enemies;

    public Transform target;
    public float Range = 2f;
    public float TurnSpeed = 1f;
    public GameObject upperturret;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++) {
            if (transform.GetChild(i).tag == "UpperTurret") upperturret = transform.GetChild(i).gameObject;
        }
    }

    void Update() {
        enemies = GameObject.FindGameObjectsWithTag(enemytag);
        if (enemies.Length < 0) return;

        GameObject closest = FindClosest(enemies);
        if (closest == null) return;

        target = closest.transform;
        LookAt(target.position);

    }       

    GameObject FindClosest(GameObject[] gameObjects) {
        GameObject closest = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject i in gameObjects) {
            float distToI = Vector3.Distance(transform.position, i.transform.position);
            if (distToI < shortestDistance) {
                shortestDistance = distToI;
                closest = i;
            }
        }

        if (shortestDistance <= Range) return closest;
        else return null;
    }

    void LookAt(Vector3 pos) {
        Vector3 dir = pos - transform.position;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        Vector3 rot = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * TurnSpeed).eulerAngles;
        rot.y = Mathf.Floor(rot.y * 10)/ 10;

        transform.rotation = Quaternion.Euler(new Vector3(-90f, rot.y, 0));
    }
}