using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 6f;
    public float gravity = -9.81f; 
    public float jumpHeight = 1.5f;

    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Update()
    {
        // Cek apakah player menyentuh tanah
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            // Reset velocity Y saat di tanah agar tidak menumpuk
            velocity.y = -2f; 
        }

        // Input pergerakan WASD
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Kalkulasi arah pergerakan berdasarkan orientasi player saat ini
        Vector3 move = transform.right * x + transform.forward * z;

        // Eksekusi pergerakan (lebih responsif tanpa efek meluncur)
        controller.Move(move * speed * Time.deltaTime);

        // Lompat dengan tombol Space
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Kalkulasi gravitasi
        velocity.y += gravity * Time.deltaTime;

        // Eksekusi pergerakan vertikal (jatuh/lompat)
        controller.Move(velocity * Time.deltaTime);
    }
}