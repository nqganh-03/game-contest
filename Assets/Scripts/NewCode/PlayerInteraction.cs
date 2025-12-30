using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Cài đặt Tương tác")]
    public float timeBonus = 15f;
    public float timeLoses = 90f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem GameManage có tồn tại không để tránh lỗi
        if (GameManage.Instance == null) return;

        // 1. Ăn Củi
        if (collision.CompareTag("Task"))
        {
            GameManage.Instance.AddTime(timeBonus);
            Destroy(collision.gameObject);
        }
        // 2. Dính Nước
        else if (collision.CompareTag("Water"))
        {
            GameManage.Instance.AddTime(-timeLoses);
            Destroy(collision.gameObject);
        }
        // 3. Ăn Sao (ĐÃ SỬA)
        else if (collision.CompareTag("Star"))
        {
            // Gọi hàm CollectStar bên GameManage
            GameManage.Instance.CollectStar();

            // Phá hủy ngôi sao
            Destroy(collision.gameObject);
        }
    }
}