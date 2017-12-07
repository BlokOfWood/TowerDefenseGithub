using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour {
    public Vector2[] waypoints; //Every enemy waypoint.
    public float EnemySpeed; //The speed which the enemy is moving with. It's here because this way it's centralized(it isn't used in this script).

    public GameObject PathNode; //Just a path prefab.

    void Start() {
        if (waypoints.Length < 2) Debug.Log("Waypoints must be at least 2 length");
        for (int i = 1; i < waypoints.Length; i++)
            //Checks if the way between the waypoints is diagonal. If true then debug.errorlog it and do nothing.
            //TODO: Diagonal lines.
            if ((waypoints[i - 1].x != waypoints[i].x) && (waypoints[i - 1].y != waypoints[i].y))
                Debug.LogError("The way between these two waypoints is diagonal:" + (i - 1) + ", " + i);
            else {
                GameObject pathnode = Instantiate(PathNode);
                //Sets the node position to the first node and then places it in between them
                float NodeXPos = waypoints[i - 1].x + (waypoints[i].x - waypoints[i - 1].x) / 2;
                float NodeYPos = waypoints[i - 1].y + (waypoints[i].y - waypoints[i - 1].y) / 2;

                float NodeXScale = Mathf.Abs(waypoints[i].x - waypoints[i - 1].x);
                float NodeYScale = Mathf.Abs(waypoints[i].y - waypoints[i - 1].y);

                if (NodeXScale == 0) {
                    //If there is no distance between the two nodes then set the scale to the default of 1.
                    NodeXScale = 1f;
                    
                    //This makes the nodes perfectly overlap so it looks good.
                    NodeYScale += 0.5f;
                    NodeYPos += 0.25f;
                }
                if (NodeYScale == 0) {
                    //Refer to the comments above.
                    NodeYScale = 1f;

                    NodeXScale += 0.5f;
                    NodeXPos += 0.25f;
                }

                pathnode.transform.position = new Vector3(NodeXPos, 0, NodeYPos);
                pathnode.transform.localScale = new Vector3(NodeXScale, 0.1f, NodeYScale);
            }
    }

    ///<summary>Just converts a Vector 2 to a Vector 3</summary>
    public Vector3 Vec2toVec3(Vector2 input) {
        Vector3 output = new Vector3(input.x, 0.55f, input.y);
        return output;
    }
}