using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    public static Game Instance;

    float power;
    public float Power {
        get { return power; }
        set { power = Mathf.Clamp(value, 0, 100); }
    }

    float oxygen;
    public float Oxygen {
        get { return oxygen; }
        set {
            if (timeSinceHurt > 2.0f) {
                oxygen = Mathf.Clamp(value, 0, 100);
            }
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
    public RectTransform OxygenBar;
    public RectTransform PowerBar;
    public Image FadeOut;
    public TextMeshProUGUI BubblitisText;
    private float initialOxygenX;
    private float initialPowerX;
    private float initialOxygenWidth;
    private float initialPowerWidth;
    public float timeSinceHurt = 100.0f;

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
    }

    // Update is called once per frame
    void Update() {
        timeSinceHurt += Time.deltaTime;
        oxygen -= Time.deltaTime * 0.25f;
        power -= Time.deltaTime * 0.25f;
        OxygenBar.anchorMax = new Vector2(initialOxygenX - ((100f - oxygen) / 100f) * initialOxygenWidth, OxygenBar.anchorMax.y);
        PowerBar.anchorMax = new Vector2(initialPowerX - ((100f - power) / 100f) * initialPowerWidth, PowerBar.anchorMax.y);
        if (oxygen <= 0) {
            var c = FadeOut.color;
            c.a += 0.2f * Time.deltaTime;
            FadeOut.color = c;
            if (c.a >= 1.0f) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(0);
            }
        }
        if (power < 0) {
            // Shut off HUD and light
            HUD.SetActive(false);
            Light.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Menu.SetActive(!Menu.activeSelf);
            if (Menu.activeSelf) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void CloseMenu() {
        Menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
