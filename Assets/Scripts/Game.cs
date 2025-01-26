using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public static Game Instance;

    float power;
    public float Power {
        get { return power; }
        set { 
            power = Mathf.Clamp(value, 0, 100);
            PowerText.text = $"{(int)power}%";
        }
    }

    float oxygen;
    public float Oxygen {
        get { return oxygen; }
        set {
            oxygen = Mathf.Clamp(value, 0, 100);
            OxygenText.text = $"Oxygen Level: {(int)oxygen}%";
        }
    }

    int bubblitisCount;
    public int BubblitisCount {
        get { return bubblitisCount; }
        set {
            bubblitisCount = value;
            BubblitisText.text = $"Bubblitis: {value}";
        }
    }

    public PlayerController PlayerController;
    public GameObject HUD;
    public GameObject Light;
    public GameObject Menu;
    public GameObject WinScreen;
    public GameObject DeathScreen;
    public RectTransform OxygenBar;
    public RectTransform PowerBar;
    public Image FadeOut;
    public TextMeshProUGUI BubblitisText;
    public TextMeshProUGUI PowerText;
    public TextMeshProUGUI OxygenText;
    private float initialOxygenX;
    private float initialPowerX;
    private float initialOxygenWidth;
    private float initialPowerWidth;
    public float timeSinceHurt = 100.0f;
    public int Won = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() {
        power = 100f;
        oxygen = 100f;
        Instance = this;
        CloseMenu();
        initialOxygenX = OxygenBar.anchorMax.x;
        initialPowerX = PowerBar.anchorMax.x;
        initialOxygenWidth = initialOxygenX - OxygenBar.anchorMin.x;
        initialPowerWidth = initialPowerX - PowerBar.anchorMin.x;
        WinScreen.SetActive(false);
        DeathScreen.SetActive(false);
    }

    bool GameOver => oxygen <= 0 || bubblitisCount <= 0;

    // Update is called once per frame
    void Update() {
        timeSinceHurt += Time.deltaTime;
        Oxygen -= Time.deltaTime * 0.25f;
        Power -= Time.deltaTime * 0.25f;
        OxygenBar.anchorMax = new Vector2(initialOxygenX - ((100f - oxygen) / 100f) * initialOxygenWidth, OxygenBar.anchorMax.y);
        PowerBar.anchorMax = new Vector2(initialPowerX - ((100f - power) / 100f) * initialPowerWidth, PowerBar.anchorMax.y);
        if (GameOver) {
            var c = FadeOut.color;
            c.a += 0.2f * Time.deltaTime;
            FadeOut.color = c;
            if (c.a >= 1.0f) {
                if(oxygen <= 0) 
                {
                    Won = -1;
                    DeathScreen.SetActive(true);
                } else
                {
                    Won = 1;
                    WinScreen.SetActive(true);
                }
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                SceneManager.LoadScene("GameOver");
            }
        } else {
            var c = FadeOut.color;
            c.a = 0.0f;
            FadeOut.color = c;
        }
        if (power <= 0) {
            // Shut off HUD and light
            HUD.SetActive(false);
            Light.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Menu.activeSelf) {
                CloseMenu();
            } else {
                Cursor.lockState = CursorLockMode.None;
                Menu.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    public void CloseMenu() {
        Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }
}
