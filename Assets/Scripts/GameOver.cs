using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject DeathScreen;
    public AudioSource WinMusic;
    public AudioSource DeathMusic;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Game.Instance.Won == 1)
        {
            WinScreen.SetActive(true);
            DeathScreen.SetActive(false);
            WinMusic.Play();
        }
        else
        {
            DeathScreen.SetActive(true);
            WinScreen.SetActive(false);
            DeathMusic.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
