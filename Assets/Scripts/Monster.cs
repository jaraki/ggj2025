using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

    public List<GameObject> healthObjects = new List<GameObject>();
    PlayerController player;
    NavMeshAgent agent;
    public LayerMask targetingMask;
    float attackTimer = 0.0f;
    public bool isZombie = false;
    Animator anim;
    Rigidbody rigid;
    float damageTimer = 0.0f;
    float soundTimer = 0.0f;

    void Start() {
        soundTimer = Random.Range(10.0f, 20.0f);
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        anim.speed = Random.Range(0.9f, 1.1f);
        player = FindFirstObjectByType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
        Game.Instance.BubblitisCount += healthObjects.Count;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("Bubble")) {
            var bub = collision.collider.GetComponent<Bubble>();
            bub.Die();

            int lastIndex = healthObjects.Count - 1;
            var last = healthObjects[lastIndex];
            last.SetActive(false);
            if (!isZombie || Random.value < 0.25f) {
                healthObjects.RemoveAt(lastIndex);
                Game.Instance.BubblitisCount--;
                if (isZombie) {
                    anim.SetTrigger("damaged");
                    if (damageTimer < -3.0f) {
                        damageTimer = 3.0f;
                    }
                    AudioManager.Instance.PlaySound(transform.position + Vector3.up, AudioManager.Instance.zombieDamage, 1.0f, Random.Range(0.5f, 0.6f));
                }
            }
            if (lastIndex == 0) {
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionStay(Collision collision) {
        if (collision.collider.CompareTag("Player")) {
            if (attackTimer < 0.0f) {
                attackTimer = 2.0f;

                Game.Instance.Oxygen -= isZombie ? 25.0f : 5.0f;
                Game.Instance.timeSinceHurt = 0.0f;
            }
        }
    }

    void Update() {
        soundTimer -= Time.deltaTime;
        damageTimer -= Time.deltaTime;
        attackTimer -= Time.deltaTime;
        Vector3 start = transform.position + Vector3.up * 0.25f;
        if (damageTimer > 0f ||
            (Physics.Raycast(start, player.transform.position - start, out RaycastHit info, 10.0f, targetingMask) && info.collider.CompareTag("Player"))) {
            agent.destination = player.transform.position;
        }

        if (isZombie) {
            anim.SetFloat("speed", agent.velocity.magnitude);
            agent.speed = damageTimer < 0.0f ? 2.5f : 1.5f;
        }

        if (soundTimer < 0.0f) {
            if (isZombie) {
                soundTimer = Random.Range(8.0f, 15.0f);
                AudioManager.Instance.PlayRandomSoundFromList(transform.position + Vector3.up, AudioManager.Instance.zombieSounds, 1.5f, Random.Range(0.8f, 1.2f));
            } else {
                soundTimer = Random.Range(3.0f, 6.0f);
                AudioManager.Instance.PlayRandomSoundFromList(transform.position, AudioManager.Instance.ratSounds, .65f, Random.Range(0.8f, 1.2f));
            }

        }

    }
}
