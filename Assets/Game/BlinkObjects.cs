using System.Collections;
using UnityEngine;

public class BlinkObjects : MonoBehaviour {

    public GameObject toBlink;

    void Start() {
        StartCoroutine(StartCoroutineExample()); // Start the coroutine when the game object starts
    }
    IEnumerator StartCoroutineExample() {
        yield return new WaitForSeconds(1f);
        toBlink.SetActive(!toBlink.activeSelf);
        StartCoroutine(StartCoroutineExample());
    }

}
