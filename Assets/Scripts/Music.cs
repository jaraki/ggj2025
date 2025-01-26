using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class Music : MonoBehaviour {
    public AudioSource IntroLoop;
    public AudioSource AmbientLoop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static Music Instance;

    void Start() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(FadeInMusic(IntroLoop, 3.0f, 0.2f));
            StartCoroutine(FadeInMusic(AmbientLoop, 3.0f, 0.1f));
        } else if (Instance != this) {
            Destroy(gameObject);
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            StartCoroutine(FadeInMusic(IntroLoop, 3.0f, 0.2f));
        }
    }

    public IEnumerator FadeInMusic(AudioSource source, float overTime, float volume) {
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime / overTime;
            source.volume = t * volume;
            yield return null;
        }
        source.volume = volume;
    }

    public IEnumerator FadeOutMusic(AudioSource source, float overTime, float volume) {
        float t = 0.0f;
        while (t < 1.0f) {
            t += Time.deltaTime / overTime;
            source.volume = (1.0f - t) * volume;
            yield return null;
        }
        source.volume = 0.0f;
    }

}
