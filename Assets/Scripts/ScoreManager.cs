using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Star Tracking")]
    public int starsCollected = 0;
    public int totalStarsInLevel = 2; // How many stars are placed in the level

    [Header("UI References")]
    public TMP_Text starCountText; // Shows "Stars: 0/2" during gameplay
    public GameObject[] starUIIcons; // Array of star icons to show/hide in UI

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Count all stars in the level automatically
        Star[] starsInScene = FindObjectsOfType<Star>();
        totalStarsInLevel = starsInScene.Length;

        starsCollected = 0;
        UpdateUI();

        Debug.Log($"ScoreManager initialized. Total stars in level: {totalStarsInLevel}");
    }

    public void CollectStar(int value = 1)
    {
        starsCollected += value;
        starsCollected = Mathf.Clamp(starsCollected, 0, totalStarsInLevel);

        UpdateUI();

        Debug.Log($"Stars collected: {starsCollected}/{totalStarsInLevel}");
    }

    void UpdateUI()
    {
        // Update text display
        if (starCountText != null)
        {
            starCountText.text = $"Stars: {starsCollected}/{totalStarsInLevel}";
        }

        // Update star icons (light up collected stars)
        if (starUIIcons != null && starUIIcons.Length > 0)
        {
            for (int i = 0; i < starUIIcons.Length; i++)
            {
                if (starUIIcons[i] != null)
                {
                    // Show/activate icon if star is collected
                    starUIIcons[i].SetActive(i < starsCollected);
                }
            }
        }
    }

    // Calculate final star rating (1-3 stars)
    public int GetFinalStarRating()
    {
        // Base star for completing the level
        int rating = 1;

        // Add collected stars
        rating += starsCollected;

        // Maximum 3 stars
        return Mathf.Min(rating, 3);
    }

    public int GetStarsCollected()
    {
        return starsCollected;
    }

    public int GetTotalStars()
    {
        return totalStarsInLevel;
    }
}