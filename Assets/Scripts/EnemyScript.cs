using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    int CurrentWaypoint = 1;
    public float speed;
    MapGen mginst;
    Vector2[] waypoints;

    void Start() {
        mginst = GameObject.Find("GameManager").GetComponent<MapGen>();
        waypoints = mginst.waypoints;
        transform.position = mginst.Vec2toVec3(waypoints[0]);
        speed = mginst.EnemySpeed;
    }

    void Update() { 
        //Runs untill there is no Waypoint left
        if (!(CurrentWaypoint == waypoints.Length)) {
            Vector3 dir = mginst.Vec2toVec3(waypoints[CurrentWaypoint]) - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);

            //This happens the enemy reaches a Waypoint
            if (Mathf.Abs(transform.position.x - mginst.Vec2toVec3(waypoints[CurrentWaypoint]).x) < 0.02 && Mathf.Abs(transform.position.y - mginst.Vec2toVec3(waypoints[CurrentWaypoint]).y) < 0.02) {
                transform.position = mginst.Vec2toVec3(waypoints[CurrentWaypoint]);
                Debug.Log("Reached Waypoint");
                CurrentWaypoint += 1;   
            }
        }
    }
}