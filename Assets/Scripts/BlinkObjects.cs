using System.Collections;
using UnityEngine;

public class BlinkObjects : MonoBehaviour {

    public GameObject toBlink;
    public float Offset = 0.0f;
    public float OnTime = 0.5f;
    public float OffTime = 3.0f;

    void Start() {
        //StartCoroutine(BlinkRoutine()); // Start the coroutine when the game object starts
    }

    float timer = 0.0f;
    void Update() {
        if(Offset > 0.0f) {
            Offset -= Time.deltaTime;
            return;
        }
        timer += Time.deltaTime;
        if(toBlink.activeSelf && timer > OnTime) {
            toBlink.SetActive(false);
            timer = 0.0f;
        }
        if(!toBlink.activeSelf && timer > OffTime) {
            toBlink.SetActive(true);
            timer = 0.0f;
        }
    }

    //IEnumerator BlinkRoutine() {
    //    while (true) {
    //        if (Offset > 0.0f) {
    //            yield return new WaitForSeconds(Offset);
    //            Offset = 0.0f;
    //        }
    //        toBlink.SetActive(true);
    //        yield return new WaitForSeconds(OnTime);
    //        toBlink.SetActive(false);
    //        yield return new WaitForSeconds(OffTime);
    //    }
    //}

}
