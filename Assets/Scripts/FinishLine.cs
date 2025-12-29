using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Cần đảm bảo GameObject này có Collider2D và Is Trigger được bật.

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra xem đối tượng va chạm có phải là Player không
        // (Giả định Player có tag là "Player")
        if (other.CompareTag("Player"))
        {
            // Gọi phương thức WinGame() trong GameManager để kết thúc game
            if (GameManager.Instance != null)
            {
                GameManager.Instance.WinGame();
                Debug.Log("Player reached the finish line! Game Win.");

                // Tùy chọn: Vô hiệu hóa điểm đích sau khi Player chạm vào
                // gameObject.SetActive(false); 
            }
        }
    }
}