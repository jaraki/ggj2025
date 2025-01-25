using UnityEngine;

public class Bubble : MonoBehaviour {

    Rigidbody rb;
    Light light;
    public float maxLifeTime = 5.0f;
    float lifeTime;
    Renderer rend;
    ParticleSystem ps;
    bool die = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        lifeTime = maxLifeTime;
        rb = GetComponent<Rigidbody>();
        light = GetComponentInChildren<Light>();
        rend = GetComponentInChildren<Renderer>();
        ps = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (die) {
            return;
        }
        rb.AddForce(Vector3.down * 0.5f, ForceMode.Acceleration);
        lifeTime -= Time.deltaTime;
        if (light) {
            light.intensity = Mathf.Lerp(0.0f, 1.0f, lifeTime / maxLifeTime);
        }
        if (lifeTime < 0.0f) {
            Destroy(gameObject, 1.0f);
            ps.Play();
            rend.enabled = false;
            die = true;
        }
    }
}
