using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    // Tạo một danh sách chọn để bạn dễ chỉnh trong Unity
    public enum MovementAxis
    {
        Horizontal, // Đi ngang (Trái - Phải)
        Vertical,   // Đi dọc (Lên - Xuống)
        Diagonal    // Đi chéo (Tùy chọn nâng cao)
    }

    [Header("Cài đặt chung")]
    public MovementAxis axis = MovementAxis.Horizontal; // Mặc định là đi ngang
    public float moveRange = 3f; // Quãng đường di chuyển
    public float moveSpeed = 3f;

    [Header("Cài đặt Xoay")]
    public bool isRotating = true;
    public float rotationSpeed = 360f;

    private Vector3 startPosition;
    private int direction = 1;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        // 1. Tính toán lượng di chuyển
        float movement = moveSpeed * Time.deltaTime * direction;

        // 2. Kiểm tra xem bạn đang chọn chế độ nào
        switch (axis)
        {
            case MovementAxis.Horizontal:
                HandleHorizontalMovement(movement);
                break;
            case MovementAxis.Vertical:
                HandleVerticalMovement(movement);
                break;
            case MovementAxis.Diagonal:
                // Đi chéo thì cộng cả X và Y
                HandleHorizontalMovement(movement);
                HandleVerticalMovement(movement);
                break;
        }

        // 3. Xoay tròn
        if (isRotating)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
    }

    // Logic đi Ngang
    void HandleHorizontalMovement(float moveStep)
    {
        transform.Translate(moveStep, 0, 0, Space.World);

        if (transform.position.x > startPosition.x + moveRange)
            direction = -1; // Quay đầu
        else if (transform.position.x < startPosition.x - moveRange)
            direction = 1;  // Quay đầu
    }

    // Logic đi Dọc (Lên xuống)
    void HandleVerticalMovement(float moveStep)
    {
        transform.Translate(0, moveStep, 0, Space.World);

        if (transform.position.y > startPosition.y + moveRange)
            direction = -1; // Quay đầu xuống
        else if (transform.position.y < startPosition.y - moveRange)
            direction = 1;  // Quay đầu lên
    }

    // Vẽ đường hướng dẫn (Gizmos) để bạn dễ sắp xếp màn chơi
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector3 center = Application.isPlaying ? startPosition : transform.position;

        if (axis == MovementAxis.Horizontal || axis == MovementAxis.Diagonal)
        {
            Gizmos.DrawLine(center + Vector3.left * moveRange, center + Vector3.right * moveRange);
        }

        if (axis == MovementAxis.Vertical || axis == MovementAxis.Diagonal)
        {
            Gizmos.DrawLine(center + Vector3.down * moveRange, center + Vector3.up * moveRange);
        }

        // Vẽ 2 quả cầu nhỏ ở điểm đầu và cuối để dễ nhìn
        if (axis == MovementAxis.Horizontal)
        {
            Gizmos.DrawWireSphere(center + Vector3.left * moveRange, 0.2f);
            Gizmos.DrawWireSphere(center + Vector3.right * moveRange, 0.2f);
        }
        else if (axis == MovementAxis.Vertical)
        {
            Gizmos.DrawWireSphere(center + Vector3.down * moveRange, 0.2f);
            Gizmos.DrawWireSphere(center + Vector3.up * moveRange, 0.2f);
        }
    }
}

