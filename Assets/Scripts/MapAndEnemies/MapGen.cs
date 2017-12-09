using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour {
    public Vector2 MapSize;
    public Vector2[] waypoints; //Every enemy waypoint.
    public float EnemySpeed; //The speed which the enemy is moving with. It's here because this way it's centralized(it isn't used in this script).

    public GameObject EnemyPathNodeParent;
    public GameObject EnemyPathNode; //Just a path prefab.

    public GameObject BaseNodeParent;
    public GameObject BaseNode;

    public GameObject StartNode; //It might become a portal or something.
    public GameObject EndNode; //It might become whatever, something creative.

    public bool noErrors = true;

    void Start() {
        if (waypoints.Length < 2) {
            Debug.LogError("There must be at least two waypoints.");
            noErrors = false;
        }
        if (waypoints[0].x >= MapSize.x || waypoints[0].y >= MapSize.y) {
            noErrors = false;
            Debug.LogError("Enemy waypoint outside of map.");
        }

        for (int i = 1; i < waypoints.Length; i++) {
            Vector2 cwp = waypoints[i]; //Current Waypoint
            Vector2 lwp = waypoints[i - 1]; //Last Waypoint

            //Checks if the way between the waypoints is diagonal. If true then debug.errorlog it and do nothing.
            if (lwp.x != waypoints[i].x && lwp.y != waypoints[i].y) {
                Debug.LogError("The way between these two waypoints is diagonal:" + (i - 1) + ", " + i);
                noErrors = false;
            }
            //Checks if waypoint is outside of map.
            if (cwp.x >= MapSize.x || cwp.y >= MapSize.y) {
                noErrors = false;
                Debug.LogError("The enemy waypoint is outside of map.");
            }


            if (noErrors) {
                GameObject pathnode = Instantiate(EnemyPathNode, EnemyPathNodeParent.transform);
                //Sets the node position to the first node and then places it in between them
                float NodeXPos = lwp.x + (cwp.x - lwp.x) / 2;
                float NodeYPos = lwp.y + (cwp.y - lwp.y) / 2;

                float NodeXScale = Mathf.Abs(cwp.x - lwp.x);
                float NodeYScale = Mathf.Abs(cwp.y - lwp.y);

                if (NodeXScale == 0) {
                    //If there is no distance between the two nodes then set the scale to the default of 1.
                    NodeXScale = 0.9f;

                    //This makes the nodes perfectly overlap so it looks good.
                    NodeYScale += 0.9f;
                }
                if (NodeYScale == 0) {
                    //Refer to the comments above.
                    NodeYScale = 0.9f;

                    NodeXScale += 0.9f;
                }

                pathnode.transform.position = new Vector3(NodeXPos, 0, NodeYPos);
                pathnode.transform.localScale = new Vector3(NodeXScale, 0.1f, NodeYScale);
            }
        }

        
        //Generates nodes for each place in the grid
        for (int x = 0; x < MapSize.x; x++) {
            for (int y = 0; y < MapSize.y; y++) {
                GameObject inst = Instantiate(BaseNode, BaseNodeParent.transform);
                inst.transform.position = new Vector3(x, 0, y);
                inst.layer = 8;
            }
        }

        StartNode.transform.position = new Vector3(waypoints[0].x, 0.55f, waypoints[0].y);
        EndNode.transform.position = new Vector3(waypoints[waypoints.Length-1].x, 0.55f, waypoints[waypoints.Length-1].y);
    }

    ///<summary>Just converts a Vector 2 to a Vector 3</summary>
    public Vector3 Vec2toVec3(Vector2 input) {
        Vector3 output = new Vector3(input.x, 0.55f, input.y);
        return output;
    }
}