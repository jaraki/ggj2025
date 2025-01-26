using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource IntroMusic;
    public AudioSource IntroLoop;
    public AudioSource AmbientMusic;
    public AudioSource AmbientLoop;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return StartCoroutine(MusicCoroutine());
        yield return StartCoroutine(AmbientCoroutine());
    }

    IEnumerator MusicCoroutine()
    {
        // for some reason it is not transitioning perfectly
        var duration = IntroMusic.clip.length - 0.2f;
        yield return new WaitForSeconds(duration);
        IntroLoop.Play();
    }
    IEnumerator AmbientCoroutine()
    {
        yield return new WaitForSeconds(AmbientMusic.clip.length);
        AmbientLoop.Play();
    }
}
