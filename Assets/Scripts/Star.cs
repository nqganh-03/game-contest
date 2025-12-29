using UnityEngine;

public class Star : MonoBehaviour
{
    [Header("Star Settings")]
    public int starValue = 1; // Usually 1, but could be used for bonus stars
    public float timeBonus = 5f; // Optional: add time when collecting star

    [Header("Effects")]
    public AudioClip collectSfx;
    public GameObject collectEffect; // Optional particle effect

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Notify ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.CollectStar(starValue);
            }

            // Optional: Add time bonus
            if (timeBonus > 0 && TimerManager.Instance != null)
            {
                TimerManager.Instance.AddTime(timeBonus);
            }

            // Play sound effect
            if (collectSfx != null)
            {
                AudioSource.PlayClipAtPoint(collectSfx, transform.position);
            }

            // Spawn particle effect
            if (collectEffect != null)
            {
                Instantiate(collectEffect, transform.position, Quaternion.identity);
            }

            Debug.Log($"Star collected! Value: {starValue}");

            // Remove star from scene
            Destroy(gameObject);
        }
    }
}