using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    public List<GameObject> healthObjects = new List<GameObject>();

    void Start() {

    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Bubble")) {
            var bub = collision.collider.GetComponent<Bubble>();
            bub.Die();

            int lastIndex = healthObjects.Count - 1;
            var last = healthObjects[lastIndex];
            last.SetActive(false);
            healthObjects.RemoveAt(lastIndex);
            if (lastIndex == 0) {
                Destroy(gameObject);
            }
        }
    }

}
