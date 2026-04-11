using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float runSpeed = 7f;
    public float crouchSpeed = 2f;
    public float jumpForce = 5f;

    [Header("Mouse")]
    public float mouseSensitivity = 100f;
    public Transform cameraPivot;

    [Header("Cameras")]
    public Transform fpsCamera;
    public Transform tpsCamera;

    [Header("TPS")]
    public float smoothSpeed = 10f;
    bool isTPS = false;

    [Header("Stamina")]
    public float stamina = 5f;
    public float maxStamina = 5f;
    public float staminaDrain = 1f;
    public float staminaRecover = 0.5f;

    Rigidbody rb;
    CapsuleCollider col;

    float xRotation;
    bool isRunning;
    bool isCrouching;
    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseLook();
        Movement();
        Jump();
        Crouch();
        Stamina();
        CameraSwitch();
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        cameraPivot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        isRunning = Input.GetKey(KeyCode.LeftShift) && stamina > 0;

        float speed = walkSpeed;
        if (isRunning) speed = runSpeed;
        if (isCrouching) speed = crouchSpeed;

        Vector3 move = transform.right * x + transform.forward * z;

        rb.linearVelocity = new Vector3(
            move.x * speed,
            rb.linearVelocity.y,
            move.z * speed
        );
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            if (isCrouching)
                col.height = 1f;
            else
                col.height = 2f;
        }
    }

    void Stamina()
    {
        if (isRunning)
            stamina -= staminaDrain * Time.deltaTime;
        else
            stamina += staminaRecover * Time.deltaTime;

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }

    void CameraSwitch()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            isTPS = !isTPS;

            fpsCamera.gameObject.SetActive(!isTPS);
            tpsCamera.gameObject.SetActive(isTPS);
        }
    }

    void LateUpdate()
    {
        if (!isTPS) return;

        Vector3 desiredPos =
            transform.position +
            transform.right * 0.5f +
            Vector3.up * 1.6f -
            transform.forward * 3f;

        tpsCamera.position = Vector3.Lerp(
            tpsCamera.position,
            desiredPos,
            smoothSpeed * Time.deltaTime
        );

        tpsCamera.LookAt(transform.position + Vector3.up * 1.5f);
    }

    void OnCollisionStay(Collision c)
    {
        isGrounded = true;
    }

    void OnCollisionExit(Collision c)
    {
        isGrounded = false;
    }
}
