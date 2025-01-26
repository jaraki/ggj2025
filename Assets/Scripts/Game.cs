using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    public float Power;
    public float Oxygen;
    public PlayerController PlayerController;
    public GameObject HUD;
    public GameObject Light;
    public GameObject Menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Power = 100f;
        Oxygen = 100f;
        Instance = this;
        CloseMenu();
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(!Menu.activeSelf);
            if(Menu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void CloseMenu()
    {
        Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
