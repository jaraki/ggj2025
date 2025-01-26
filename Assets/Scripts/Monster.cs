using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

    public List<GameObject> healthObjects = new List<GameObject>();
    PlayerController player;
    NavMeshAgent agent;
    public LayerMask targetingMask;
    float attackTimer = 0.0f;

    void Start() {
        var anim = GetComponentInChildren<Animator>();
        anim.speed = Random.Range(0.9f, 1.1f);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
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

    void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            if (attackTimer < 0.0f) {
                attackTimer = 2.0f;
                Game.Instance.Oxygen -= 5.0f;
            }
        }
    }

    void Update() {
        attackTimer -= Time.deltaTime;
        Vector3 start = transform.position + Vector3.up * 0.25f;
        if (Physics.Raycast(start, player.transform.position - start, out RaycastHit info, 10.0f, targetingMask)) {
            if (info.collider.CompareTag("Player")) {
                agent.destination = player.transform.position;
            }
        }

    }

}
