using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [Header("Pause Panel")]
    public GameObject pausePanel;

    [Header("Pause Panel Buttons (inside pause panel)")]
    public Button resumeButton;
    public Button restartButton;
    public Button menuButton;

    private bool isPaused = false;

    void Start()
    {
        // Setup button listeners for buttons INSIDE pause panel
        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);

        if (restartButton != null)
            restartButton.onClick.AddListener(RestartLevel);

        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMenu);

        // Hide pause panel at start
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        // Press ESC to pause/unpause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (pausePanel != null)
            pausePanel.SetActive(isPaused);

        // Freeze/unfreeze time
        Time.timeScale = isPaused ? 0f : 1f;

        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
            pausePanel.SetActive(false);

        Time.timeScale = 1f;

        Debug.Log("Game Resumed");
    }

    void RestartLevel()
    {
        // Unpause before restarting
        Time.timeScale = 1f;
        isPaused = false;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartLevel();
        }
        else if (LevelManager.Instance != null)
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

    void GoToMenu()
    {
        // Unpause before going to menu
        Time.timeScale = 1f;
        isPaused = false;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadMainMenu();
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}