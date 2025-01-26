using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public float Power;
    public float Oxygen;
    public PlayerController PlayerController;
    public GameObject HUD;
    public GameObject Light;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Power = 100f;
        Oxygen = 100f;
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Oxygen -= Time.deltaTime * 0.25f;
        Power -= Time.deltaTime * 0.25f;
        if(Oxygen < 0 )
        {
            // Game Over

        }
        if (Power < 0)
        {
            // Shut off HUD and light
            HUD.SetActive(false);
            Light.SetActive(false);
        }
    }
}
