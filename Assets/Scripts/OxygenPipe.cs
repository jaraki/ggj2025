using UnityEngine;

public class OxygenPipe : MonoBehaviour {

    private void OnCollisionStay(Collision collision) {

    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            Game.Instance.Oxygen += Time.deltaTime * 20.0f;
        }
    }
}
