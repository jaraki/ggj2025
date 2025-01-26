using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager Instance;

    public List<AudioSource> sources = new List<AudioSource>();

    public List<AudioClip> zombieSounds;
    public List<AudioClip> ratSounds;

    public AudioClip zombieDamage;
    public AudioClip walking;

    public AudioClip bubble1;
    public AudioClip bubble2;

    public AudioClip playerHurt;

    void Awake() {
        Instance = this;
        for (int i = 0; i < 32; ++i) {
            GameObject go = new GameObject();
            go.transform.parent = transform;
            var source = go.AddComponent<AudioSource>();
            source.spatialBlend = 1.0f;
            sources.Add(source);
        }

    }

    public void PlayRandomSoundFromList(Vector3 pos, List<AudioClip> clips, float volume, float pitch) {
        var clip = clips[Random.Range(0, clips.Count)];
        PlaySound(pos, clip, volume, pitch);
    }

    public void PlaySound(Vector3 pos, AudioClip clip, float volume, float pitch){
        foreach (var source in sources) {
            if (!source.isPlaying) {
                source.transform.position = pos;
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
                break;
            }
        }
    }

}
