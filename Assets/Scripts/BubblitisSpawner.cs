using UnityEngine;

public class BubblitisSpawner : MonoBehaviour {

    public GameObject bubblitisPrefab;
    public LayerMask mask;
    public int count = 100;
    public float scaleMin = 0.6f;
    public float scaleMax = 1.4f;
    public float range = 100.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        for (int i = 0; i < count; i++) {
            if (Physics.Raycast(transform.position, Random.onUnitSphere * range, out RaycastHit hit, range, mask)) {
                var go = Instantiate(bubblitisPrefab, hit.point, Random.rotationUniform);
                Game.Instance.BubblitisCount++;
                go.transform.localScale = go.transform.localScale * Random.Range(scaleMin, scaleMax);
            }
        }
    }

}
