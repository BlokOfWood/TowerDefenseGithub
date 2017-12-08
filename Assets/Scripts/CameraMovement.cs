using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    public float CameraSpeed;

    void Update() {

        if (Input.mousePosition.x <= 0 + Screen.currentResolution.width / 96) transform.position += Vector3.left * CameraSpeed;
        if (Input.mousePosition.x >= Screen.currentResolution.width - Screen.currentResolution.width / 96) transform.position += Vector3.right * CameraSpeed;
        Debug.Log(Input.mousePosition.x);
        Debug.Log(Screen.currentResolution.width);
    }
}