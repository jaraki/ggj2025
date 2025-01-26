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
    public RectTransform OxygenBar;
    public RectTransform PowerBar;
    private float initialOxygenX;
    private float initialPowerX;
    private float initialOxygenWidth;
    private float initialPowerWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Power = 100f;
        Oxygen = 100f;
        Instance = this;
        CloseMenu();
        initialOxygenX = OxygenBar.anchorMax.x;
        initialPowerX = PowerBar.anchorMax.x;
        initialOxygenWidth = initialOxygenX - OxygenBar.anchorMin.x;
        initialPowerWidth = initialPowerX - PowerBar.anchorMin.x;
    }

    // Update is called once per frame
    void Update()
    {
        Oxygen -= Time.deltaTime * 0.25f;
        Power -= Time.deltaTime * 0.25f;
        OxygenBar.anchorMax = new Vector2(initialOxygenX - ((100f - Oxygen) / 100f) * initialOxygenWidth, OxygenBar.anchorMax.y);
        PowerBar.anchorMax = new Vector2(initialPowerX - ((100f - Power) / 100f) * initialPowerWidth, PowerBar.anchorMax.y);
        if (Oxygen < 0 )
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
