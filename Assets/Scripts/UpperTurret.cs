using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperTurret : MonoBehaviour {
    private void Update()
    {
        LookAtTarget(gameObject.GetComponentInParent<MinigunTurret>().target.position);
    }

    public void LookAtTarget(Vector3 target) {
        Vector3 dir = target - transform.position;
        Quaternion upperturretlookrot = Quaternion.LookRotation(dir);
        Vector3 vec3lookrot = upperturretlookrot.eulerAngles;
        Debug.Log(UsefulScripts.Vec3toString(vec3lookrot));

        transform.localRotation = Quaternion.Euler(new Vector3(vec3lookrot.x, 0, -180f));
    }
}