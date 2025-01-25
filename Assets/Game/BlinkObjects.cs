using System.Collections;
using UnityEngine;

public class BlinkObjects : MonoBehaviour {

    public GameObject toBlink;

    void Start() {
        StartCoroutine(StartCoroutineExample()); // Start the coroutine when the game object starts
    }
    IEnumerator StartCoroutineExample() {
        toBlink.SetActive(true);
        yield return new WaitForSeconds(2f);
        toBlink.SetActive(false);
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(StartCoroutineExample());
    }

}
