using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isGameOver = false;

    public TMP_Text statusText;

    [Header("Win/Lose Options")]
    public bool autoLoadNextLevel = true; // Automatically load next level on win
    public float delayBeforeNextLevel = 3f; // Delay in seconds before loading next level

    [Header("Star Rating UI")]
    public GameObject[] starRatingIcons; // 3 star icons to show on win screen

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // Ẩn status text khi game bắt đầu
        if (statusText != null)
            statusText.gameObject.SetActive(false);

        // Hide star rating icons at start
        HideStarRating();

        // Reset obstacle penalty to base value when game starts
        Obstacle.ResetPenalty(2f); // 2f is the base penalty

        Debug.Log("GameManager initialized.");
    }

    // Phương thức WinGame() công khai, được gọi khi Player chạm đích
    public void WinGame()
    {
        if (isGameOver) return;

        // Calculate star rating
        int starRating = 1; // Base star for completion
        if (ScoreManager.Instance != null)
        {
            starRating = ScoreManager.Instance.GetFinalStarRating();
        }

        // Show current level info if LevelManager exists
        string levelInfo = "";
        if (LevelManager.Instance != null)
        {
            levelInfo = $" - Level {LevelManager.Instance.GetCurrentLevel()} Complete!";
        }

        // Create win message with star rating
        string message = $"YOU WIN!{levelInfo}\n★ {starRating}/3 Stars";

        EndGame(message);

        // Show star rating visually
        ShowStarRating(starRating);

        // Save star rating if using persistent data
        if (LevelManager.Instance != null)
        {
            int currentLevel = LevelManager.Instance.GetCurrentLevel();
            SaveLevelStars(currentLevel, starRating);
        }

        // Load next level after delay
        if (autoLoadNextLevel && LevelManager.Instance != null)
        {
            Invoke(nameof(LoadNextLevel), delayBeforeNextLevel);
        }
    }

    public void LoseGame()
    {
        if (isGameOver) return;
        EndGame("YOU LOSE!\nTry Again!");
    }

    void LoadNextLevel()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadNextLevel();
        }
    }

    void EndGame(string message)
    {
        isGameOver = true;

        // Vô hiệu hóa Timer
        if (TimerManager.Instance != null)
            TimerManager.Instance.enabled = false;

        // Vô hiệu hóa Player
        var player = FindObjectOfType<PlayerMovement>();
        if (player != null)
            player.enabled = false;

        // Hiển thị thông báo trạng thái
        if (statusText != null)
        {
            statusText.gameObject.SetActive(true);
            statusText.text = message;
        }

        Debug.Log("Game Over: " + message);
    }

    void ShowStarRating(int stars)
    {
        if (starRatingIcons == null || starRatingIcons.Length == 0) return;

        for (int i = 0; i < starRatingIcons.Length; i++)
        {
            if (starRatingIcons[i] != null)
            {
                // Show star icon if earned (i < stars means this star was earned)
                starRatingIcons[i].SetActive(i < stars);
            }
        }
    }

    void HideStarRating()
    {
        if (starRatingIcons == null || starRatingIcons.Length == 0) return;

        foreach (GameObject icon in starRatingIcons)
        {
            if (icon != null)
                icon.SetActive(false);
        }
    }

    void SaveLevelStars(int level, int stars)
    {
        // Get previous best score
        int previousBest = PlayerPrefs.GetInt($"Level_{level}_Stars", 0);

        // Save only if new score is better
        if (stars > previousBest)
        {
            PlayerPrefs.SetInt($"Level_{level}_Stars", stars);
            PlayerPrefs.Save();
            Debug.Log($"New best! Level {level}: {stars} stars saved.");
        }
        else
        {
            Debug.Log($"Previous best: {previousBest} stars. Current: {stars} stars.");
        }
    }

    // Public method to get saved stars for a level
    public static int GetLevelStars(int level)
    {
        return PlayerPrefs.GetInt($"Level_{level}_Stars", 0);
    }

    // Public method to restart level (can be called from UI button)
    public void RestartLevel()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RestartLevel();
        }
        else
        {
            // Fallback: reload current scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
            );
        }
    }
}