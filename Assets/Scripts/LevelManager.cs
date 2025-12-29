using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("Level Settings")]
    public int currentLevel = 1;
    public int totalLevels = 5; // total number of levels

    void Awake()
    {
        // Singleton pattern - persist across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Load next level
    public void LoadNextLevel()
    {
        currentLevel++;

        if (currentLevel <= totalLevels)
        {
            // Load scene by name (e.g., "Level1", "Level2", etc.)
            string sceneName = "Level" + currentLevel;
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Loading {sceneName}");
        }
        else
        {
            // All levels completed
            Debug.Log("All levels completed!");
            LoadGameCompleteScene();
        }
    }

    // Load a specific level
    public void LoadLevel(int levelNumber)
    {
        if (levelNumber >= 1 && levelNumber <= totalLevels)
        {
            currentLevel = levelNumber;
            string sceneName = "Level" + levelNumber;
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Loading {sceneName}");
        }
        else
        {
            Debug.LogWarning($"Level {levelNumber} doesn't exist!");
        }
    }

    // Restart current level
    public void RestartLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        Debug.Log($"Restarting {currentSceneName}");
    }

    // Load main menu
    public void LoadMainMenu()
    {
        currentLevel = 1;
        SceneManager.LoadScene("MainMenu");
    }

    // Load game complete scene
    void LoadGameCompleteScene()
    {
        SceneManager.LoadScene("GameComplete");
    }

    // Get current level number
    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}