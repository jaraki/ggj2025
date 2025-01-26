using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public Animation Animation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return StartCoroutine(CreditsCoroutine());
    }

    IEnumerator CreditsCoroutine()
    {
        yield return new WaitForSeconds(Animation.clip.length);
        SceneManager.LoadScene("Menu");
    }
}
