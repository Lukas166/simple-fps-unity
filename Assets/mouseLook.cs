using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Settings")]
    // Angka ini sekarang bisa kamu set ke angka normal seperti 1, 2, atau 3 di Inspector
    public float mouseSensitivity = 1f; 
    
    // Ini konstanta rahasianya agar raw input DPI mouse ditekan ke skala rotasi Unity (derajat)
    private float sensitivityMultiplier = 0.02f; 

    public Transform playerBody;

    [Header("Head Bobbing")]
    public float bobSpeed = 8f;
    public float bobAmount = 0.05f;

    float xRotation = 0f;
    float defaultYPos;
    float timer = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        defaultYPos = transform.localPosition.y;
    }

    void Update()
    {
        // Raw input dikali sensitivity dari Inspector, lalu dikali multiplier penyeimbang
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * sensitivityMultiplier;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * sensitivityMultiplier;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        // HEAD BOBBING SAAT BERJALAN
        float horizontal = Input.GetAxisRaw("Horizontal"); // Pakai Raw juga agar gerak WASD instan
        float vertical = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            timer += Time.deltaTime * bobSpeed;
            float newY = defaultYPos + Mathf.Sin(timer) * bobAmount;

            transform.localPosition = new Vector3(
                transform.localPosition.x,
                newY,
                transform.localPosition.z
            );
        }
        else
        {
            timer = 0;
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                Mathf.Lerp(transform.localPosition.y, defaultYPos, Time.deltaTime * 5),
                transform.localPosition.z
            );
        }
    }
}