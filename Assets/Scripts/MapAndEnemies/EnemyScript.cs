using System.Timers;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public GameObject gm;
    MapGen mginst;
    
    public EnemyType thisenemy;
    public float speed;

    int CurrentWaypoint = 1;
    Vector2[] waypoints;
    

    void Start() {
        gm = GameObject.Find("GameManager");
        mginst = gm.GetComponent<MapGen>();

        waypoints = mginst.waypoints;
        transform.position = mginst.Vec2toVec3(waypoints[0]);

        speed = thisenemy.speed;
    }

    void Update() { 
        //Runs untill there is no Waypoint left
        if (!(CurrentWaypoint == waypoints.Length)) {
            Vector3 dir = mginst.Vec2toVec3(waypoints[CurrentWaypoint]) - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime);


            float DistanceX = Mathf.Abs(transform.position.x - waypoints[CurrentWaypoint].x);
            float DistanceY = Mathf.Abs(transform.position.z - waypoints[CurrentWaypoint].y);
            
            if (DistanceX < 0.04 && DistanceY < 0.04) {
                //This happens the enemy reaches a Waypoint
                transform.position = mginst.Vec2toVec3(waypoints[CurrentWaypoint]);
                Debug.Log("Reached Waypoint");
                CurrentWaypoint += 1;   
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "EndNode") {
            gm.GetComponent<WaveManagement>().EnemyReachedEnd(thisenemy.damage);
            Destroy(gameObject);
        }
    }
}