using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTowards : MonoBehaviour {
    public Transform target;
    [Tooltip("The higher this is, the quicker it snaps")]
    public float lambda = 40.0f;

    // make camera follow player smoothly, gets rid of annoying jitter 
    // while moving (with physics in fixedupdate) and turning camera (during update)
    // could just move cam to fixedupdate but then doesnt go past 50 fps which wont look as SMOOTH
    void LateUpdate() {
        //float t = 1.0f - Mathf.Pow(0.001f, Time.deltaTime);
        float t = 1.0f - Mathf.Exp(-lambda * Time.deltaTime);
        //t = Time.deltaTime * lambda;
        transform.position = Vector3.Lerp(transform.position, target.position, t);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, t);
    }

    public void Snap() {
        transform.position = target.position;
        //transform.rotation = follow.rotation;
    }
}
