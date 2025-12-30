using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // --- PHẦN KHAI BÁO BIẾN (Bạn đang thiếu phần này) ---
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    // --- PHẦN HÀM XỬ LÝ ---
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