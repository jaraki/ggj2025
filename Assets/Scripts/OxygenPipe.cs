using UnityEngine;

public class OxygenPipe : MonoBehaviour {

    float timer = 0.0f;

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player") && Game.Instance.timeSinceHurt > 2.0f) {
            Game.Instance.Oxygen += Time.deltaTime * 25.0f;
            timer -= Time.deltaTime;
            Game.Instance.timeSinceReload = 0.0f;
            if (timer < 0.0f && Game.Instance.Oxygen < 99.0f) {
                AudioManager.Instance.PlaySound(transform.position, AudioManager.Instance.bubble1, 0.2f, Game.Instance.Oxygen / 100.0f);
                timer = 0.05f;
            }
        }
    }
}
