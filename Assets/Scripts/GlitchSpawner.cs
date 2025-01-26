using System.Collections;
using UnityEngine;

public class GlitchSpawner : MonoBehaviour
{
    public GameObject Glitch;
    public float MinRandomTime = 5;
    public float MaxRandomTime = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return StartCoroutine(SpawnGlitches());
    }

    IEnumerator SpawnGlitches()
    {
        Instantiate(Glitch);
        yield return new WaitForSeconds(Random.Range(MinRandomTime, MaxRandomTime));
        yield return StartCoroutine(SpawnGlitches());
    }
}
