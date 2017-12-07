using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgóMorgóV2 : MonoBehaviour {
    public float rotspeed;

	void Update () {
        transform.Rotate(0, rotspeed*Time.deltaTime*100, 0);
	}
}