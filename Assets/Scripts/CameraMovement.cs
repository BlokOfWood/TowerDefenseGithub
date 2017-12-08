using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float CameraSpeed;
    public float LeftBorder;
    public float RightBorder;
    public float UpperBorder;
    public float BottomBorder;

    void Start() {
        MapGen mg = GameObject.Find("GameManager").GetComponent<MapGen>();
        LeftBorder = 0;
        RightBorder = mg.MapSize.x;
        UpperBorder = mg.MapSize.y - 2;
        BottomBorder = -3;
    }

    void Update() {
        if (transform.position.x > LeftBorder) 
            if (Input.GetKey("a")) transform.position += Vector3.left * CameraSpeed * Time.deltaTime * 50f;
        if (transform.position.x < RightBorder)
            if (Input.GetKey("d")) transform.position += Vector3.right * CameraSpeed * Time.deltaTime * 50f;
        if (transform.position.z > BottomBorder)
            if (Input.GetKey("s")) transform.position += Vector3.back * CameraSpeed * Time.deltaTime * 50f;
        if (transform.position.z < UpperBorder)
            if (Input.GetKey("w")) transform.position += Vector3.forward * CameraSpeed * Time.deltaTime * 50f; 
    }
}