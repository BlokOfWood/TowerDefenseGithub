using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour {

    void Start() {
        MapGen mg = GameObject.Find("GameManager").GetComponent<MapGen>();
        GetComponent<RectTransform>().sizeDelta = new Vector2(mg.MapSize.x+2, mg.MapSize.y + 2);
	}
}
