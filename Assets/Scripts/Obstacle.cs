using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float basePenalty = 2f;
    public float penaltyIncrease = 0.5f;
    public AudioClip hitSfx;

    private static float currentPenalty = 0f;
    private static bool isInitialized = false;

    void Awake()
    {
        // Initialize penalty on first obstacle load
        if (!isInitialized)
        {
            currentPenalty = basePenalty;
            isInitialized = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Apply current penalty
            TimerManager.Instance.AddTime(-currentPenalty);

            Debug.Log($"Obstacle hit! Time penalty: {currentPenalty}s");

            if (hitSfx != null)
            {
                AudioSource.PlayClipAtPoint(hitSfx, transform.position);
            }

            // Increase penalty for next collision
            currentPenalty += penaltyIncrease;

            // Obstacle stays in place - player can pass through
        }
    }

    // Call this method to reset penalty (e.g., when restarting the game)
    public static void ResetPenalty(float baseValue)
    {
        currentPenalty = baseValue;
        isInitialized = true;
    }
}