using UnityEngine;

public class Bubble : MonoBehaviour
{

    Rigidbody rb;
    Light l;
    const float maxLifeTime = 8.0f;
    float lifeTime = maxLifeTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        l = rb.GetComponent<Light>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 0.5f, ForceMode.Acceleration);
        lifeTime -= Time.deltaTime;
        l.intensity = Mathf.Lerp(0.0f, 1.0f, lifeTime/maxLifeTime);
        if(lifeTime < 0.0f) {
            Destroy(gameObject);
        }
    }
}
