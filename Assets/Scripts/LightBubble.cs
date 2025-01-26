using UnityEngine;

public class LightBubble : MonoBehaviour {

    Rigidbody rb;
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision) {
        int layer = collision.gameObject.layer;
        if (layer == Layers.Geo || layer == Layers.LightBubble) {
            rb.isKinematic = true;
        }
    }
}
