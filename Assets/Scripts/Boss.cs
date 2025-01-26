using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour {

    public List<GameObject> healthObjects = new List<GameObject>();
    PlayerController player;
    NavMeshAgent agent;
    public LayerMask targetingMask;
    float attackTimer = 0.0f;
    Animator anim;
    Rigidbody rigid;
    float damageTimer = 0.0f;
    float soundTimer = 0.0f;
    public GameObject bubPrefab;

    public List<AudioClip> bossSounds = new List<AudioClip>();

    void Start() {
        soundTimer = Random.Range(10.0f, 20.0f);
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        anim.speed = Random.Range(0.9f, 1.1f);
        player = FindFirstObjectByType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

        var children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children) {
            var go = Instantiate(bubPrefab, child.position + Random.insideUnitSphere * 2.0f, Random.rotation, null);
            go.transform.localScale = Vector3.one * Random.Range(0.8f, 1.2f);
            go.transform.parent = child;
            healthObjects.Add(go);
        }
        Game.Instance.BubblitisCount += healthObjects.Count;

    }

    //private void OnCollisionEnter(Collision collision) {
    //    if (collision.collider.CompareTag("Bubble")) {
    //        var bub = collision.collider.GetComponent<Bubble>();
    //        bub.Die();

    //        int lastIndex = healthObjects.Count - 1;
    //        var last = healthObjects[lastIndex];
    //        last.SetActive(false);
    //        healthObjects.RemoveAt(lastIndex);
    //        Game.Instance.BubblitisCount--;
    //        anim.SetTrigger("damaged");
    //        AudioManager.Instance.PlaySound(transform.position + Vector3.up, AudioManager.Instance.zombieDamage, 1.0f, Random.Range(0.5f, 0.6f));
    //        if (lastIndex == 0) {
    //            Destroy(gameObject);
    //        }
    //    }
    //}

    void Update() {
        transform.Rotate(Vector3.up * Time.deltaTime * 20.0f); // just spin menacingly
        soundTimer -= Time.deltaTime;
        //damageTimer -= Time.deltaTime;
        //attackTimer -= Time.deltaTime;
        //Vector3 start = transform.position + Vector3.up * 0.25f;
        //if (damageTimer > 0f ||
        //    (Physics.Raycast(start, player.transform.position - start, out RaycastHit info, 10.0f, targetingMask) && info.collider.CompareTag("Player"))) {
        //    agent.destination = player.transform.position;
        //}

        if (soundTimer < 0.0f) {
            soundTimer = Random.Range(5.0f, 8.0f);
            AudioManager.Instance.PlayRandomSoundFromList(transform.position, bossSounds, 0.5f, Random.Range(0.8f, 1.2f));
        }

    }
}
