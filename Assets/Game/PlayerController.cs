using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    PlayerInput input;
    Rigidbody rigid;
    public Transform cam;
    public float lookSensitivity = 0.25f;

    public const float MAX_STEEP = 50.0f;
    public const float TRY_JUMP_LENIENCE = 0.2f;
    public float moveSpeed = 3.0f;
    public float jumpSpeed = 5.0f;
    float jumpCooldown = 0.0f;
    float timeSinceJump = 100.0f;
    float timeSinceTryJump = 100.0f;

    Collider col;
    readonly RaycastHit[] hits = new RaycastHit[16];

    InputAction moveAction;
    InputAction lookAction;
    InputAction jumpAction;
    InputAction fireAction;

    float pitch = 0.0f;

    public GameObject bubblePrefab;
    public Transform bubbleLaunch;

    void Awake() {
        Application.targetFrameRate = 60;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        rigid = GetComponent<Rigidbody>();
        col = GetComponentInChildren<Collider>();

        moveAction = InputSystem.actions.FindAction("Move");
        lookAction = InputSystem.actions.FindAction("Look");
        jumpAction = InputSystem.actions.FindAction("Jump");
        fireAction = InputSystem.actions.FindAction("Attack");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update() {
        timeSinceJump += Time.deltaTime;
        timeSinceTryJump += Time.deltaTime;
        jumpCooldown -= Time.deltaTime;

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Vector2 lookValue = lookAction.ReadValue<Vector2>() * lookSensitivity;

        // setup camera look
        pitch += lookValue.y;
        pitch = Mathf.Clamp(pitch, -89, 89); // annoying to clamp pitch if dont save variable
        transform.Rotate(0.0f, lookValue.x, 0.0f);
        cam.localRotation = Quaternion.AngleAxis(pitch, Vector3.left);

        // calc movement
        Vector3 move = transform.forward * moveValue.y + transform.right * moveValue.x;
        move = move.normalized * moveSpeed;

        float yvel = rigid.linearVelocity.y;
        Vector3 start = transform.position + Vector3.up * 0.25f;
        int count = Physics.SphereCastNonAlloc(start, 0.15f, Vector3.down, hits, 0.2f);

        bool grounded = false;
        Vector3 normal = Vector3.zero;
        for (int i = 0; i < count; ++i) {
            if (hits[i].collider != col && !hits[i].collider.isTrigger) {
                grounded = true;
                normal += hits[i].normal;
            }
        }

        bool onSteep = grounded && Vector3.Angle(Vector3.up, normal.normalized) > MAX_STEEP;
        if (onSteep) {
            move.x = rigid.linearVelocity.x;
            move.z = rigid.linearVelocity.z;
        }
        //Debug.DrawRay(transform.position, normal, onSteep ? Color.magenta : Color.green, 5.0f);
        //Debug.Log(grounded + " " + onSteep);
        if (jumpAction.triggered) {
            timeSinceTryJump = 0.0f;
        }
        if (timeSinceTryJump < TRY_JUMP_LENIENCE && grounded && !onSteep && jumpCooldown <= 0.0f) {
            grounded = false;
            yvel = jumpSpeed;
            jumpCooldown = 0.25f;
            timeSinceJump = 0.0f;
        }
        move.y = yvel;

        rigid.useGravity = true;
        if (grounded && !onSteep && timeSinceJump > 0.2f) {
            if (move.x == 0 && move.z == 0) { // dont apply gravity if grounded and no inputs
                rigid.useGravity = false;
            }
            move = Vector3.ProjectOnPlane(move, normal);
        }

        rigid.linearVelocity = move;


        if (fireAction.triggered) {
            GameObject go = Instantiate(bubblePrefab, bubbleLaunch.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().linearVelocity = bubbleLaunch.forward * 5.0f;
        }
    }

    private void OnApplicationFocus(bool focus) {
        if (focus) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.visible = true;
        }
    }
}
