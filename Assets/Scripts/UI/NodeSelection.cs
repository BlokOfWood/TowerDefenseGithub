using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelection : MonoBehaviour {
    RaycastHit hit;
    Ray ray;
    Camera cam;
    GameObject currselected;
    public float ChangeRate;
    public Color selectedcolor;
    public Color unselectedcolor;

    IEnumerator changecolortosel;
    IEnumerator changecolortounsel;

    void Start() {
        cam = Camera.main;
    }

    IEnumerator ChangeColorToSelected(GameObject selected) {
        //Same as below.
        Renderer renderer = selected.GetComponent<Renderer>();
        renderer.material.color = Color.Lerp(renderer.material.color, selectedcolor, ChangeRate);
        float RedDiff = Mathf.Abs(renderer.material.color.r - selectedcolor.r);
        float GreenDiff = Mathf.Abs(renderer.material.color.g - selectedcolor.g);
        float BlueDiff = Mathf.Abs(renderer.material.color.b - selectedcolor.b);

        float WhichWayRed = 1;
        if (selectedcolor.r - renderer.material.color.r < 0) WhichWayRed = -1;
        float WhichWayGreen = 1;
        if (selectedcolor.g - renderer.material.color.g < 0) WhichWayRed = -1;
        float WhichWayBlue = 1;
        if (selectedcolor.b - renderer.material.color.b < 0) WhichWayRed = -1;

        while (true) {
            RedDiff = Mathf.Abs(renderer.material.color.r - selectedcolor.r);
            GreenDiff = Mathf.Abs(renderer.material.color.g - selectedcolor.g);
            BlueDiff = Mathf.Abs(renderer.material.color.b - selectedcolor.b);

            if (RedDiff < 0.1f) WhichWayRed = 0;
            if (GreenDiff < 0.1f) WhichWayGreen = 0;
            if (BlueDiff < 0.1f) WhichWayBlue = 0;

            float NextRed = renderer.material.color.r + (WhichWayRed * ChangeRate);
            float NextGreen = renderer.material.color.g + (WhichWayGreen * ChangeRate);
            float NextBlue = renderer.material.color.b + (WhichWayBlue * ChangeRate);

            renderer.material.color = Color.Lerp(renderer.material.color, selectedcolor, ChangeRate);

            if (RedDiff < 0.1f && BlueDiff < 0.1f && GreenDiff < 0.1f) break;
            yield return new WaitForFixedUpdate();
        }
        renderer.material.color = selectedcolor;
        yield return new WaitForFixedUpdate();
    }

    IEnumerator ChangeColorToUnselcted(GameObject unselected) {
        Renderer renderer = unselected.GetComponent<Renderer>();
        //Initializes Diff values. Only because it probably improves performance.
        float RedDiff = Mathf.Abs(renderer.material.color.r - unselectedcolor.r);
        float GreenDiff = Mathf.Abs(renderer.material.color.g - unselectedcolor.g);
        float BlueDiff = Mathf.Abs(renderer.material.color.b - unselectedcolor.b);

        //Find out if we should add to or subtract from the value
        float WhichWayRed = 1;
        if (unselectedcolor.r - renderer.material.color.r < 0) WhichWayRed = -1;
        float WhichWayGreen = 1;
        if (unselectedcolor.g - renderer.material.color.g < 0) WhichWayRed = -1;
        float WhichWayBlue = 1;
        if (unselectedcolor.b - renderer.material.color.b < 0) WhichWayRed = -1;


        while (true) {
            //Difference between target color and current color split between every rgb channel.
            RedDiff = Mathf.Abs(renderer.material.color.r - unselectedcolor.r);
            GreenDiff = Mathf.Abs(renderer.material.color.g - unselectedcolor.g);
            BlueDiff = Mathf.Abs(renderer.material.color.b - unselectedcolor.b);
            //If a color is already what it should be then don't change it.
            if (RedDiff < 0.1f) WhichWayRed = 0;
            if (GreenDiff < 0.1f) WhichWayGreen = 0;
            if (BlueDiff < 0.1f) WhichWayBlue = 0;
            //Next color values
            float NextRed = renderer.material.color.r + (WhichWayRed * ChangeRate);
            float NextGreen = renderer.material.color.g + (WhichWayGreen * ChangeRate);
            float NextBlue = renderer.material.color.b + (WhichWayBlue * ChangeRate);
            renderer.material.color = new Color(NextRed, NextGreen, NextBlue);
            //If color is close enough then break
            if (RedDiff < 0.1f && BlueDiff < 0.1f && GreenDiff < 0.1f) break;
            yield return new WaitForFixedUpdate();
        }
        renderer.material.color = unselectedcolor;
        yield break;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit)) {                   
                GameObject hitgameobject = hit.collider.gameObject;

                if (hitgameobject.layer == 8) {
                    //Stops all coroutines. This is because this way the player can quickly click on a lot of objects.
                    StopAllCoroutines();

                    if (currselected == hitgameobject.gameObject) {
                        //If we click on a selected object deselect it.
                        changecolortounsel = ChangeColorToUnselcted(currselected);
                        StartCoroutine(changecolortounsel);
                        currselected = null;
                    }
                    else if (currselected != null) {
                        //If an object is already selected then deselct it and then select the clicked on object.
                        changecolortounsel = ChangeColorToUnselcted(currselected);
                        StartCoroutine(changecolortounsel);
                        currselected = hitgameobject.gameObject;
                        changecolortosel = ChangeColorToSelected(currselected);
                        StartCoroutine(changecolortosel);
                    }
                    else {
                        //If there isn't no object currently selected then just select the new one.
                        currselected = hitgameobject.gameObject;
                        changecolortosel = ChangeColorToSelected(currselected);
                        StartCoroutine(changecolortosel);
                    }
                }
            }
        }   
    }
}