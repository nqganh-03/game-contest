using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ chạy
    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        // Tự động tìm component Rigidbody2D trên người Player
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lấy tín hiệu từ bàn phím (Mũi tên hoặc WASD)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        // Chuẩn hóa vector (để đi chéo không bị nhanh hơn đi thẳng)
        moveInput = moveInput.normalized;
    }

    void FixedUpdate()
    {
        // Di chuyển vật lý
        rb.velocity = moveInput * moveSpeed;
    }
}