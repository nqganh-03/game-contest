using UnityEngine;

public class TaskPoint : MonoBehaviour
{
    public float timeBonus = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Add time reward
            TimerManager.Instance.AddTime(timeBonus);

            // Đã loại bỏ: Notify GameManager (to check for win condition)

            // Remove task
            Destroy(gameObject);
        }
    }
}