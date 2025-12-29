using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [Header("UI Buttons")]
    public Button startButton;
    public Button levelSelectButton;
    public Button quitButton;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;

    void Start()
    {
        // Setup button listeners
        if (startButton != null)
            startButton.onClick.AddListener(StartGame);

        if (levelSelectButton != null)
            levelSelectButton.onClick.AddListener(ShowLevelSelect);

        if (quitButton != null)
            quitButton.onClick.AddListener(QuitGame);

        // Show main menu, hide level select
        ShowMainMenu();
    }

    void StartGame()
    {
        // Start from level 1
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadLevel(1);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
        }
    }

    void ShowLevelSelect()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(false);

        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(true);
    }

    void ShowMainMenu()
    {
        if (mainMenuPanel != null)
            mainMenuPanel.SetActive(true);

        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(false);
    }

    void QuitGame()
    {
        Debug.Log("Quitting game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    // Public method for back button in level select
    public void BackToMainMenu()
    {
        ShowMainMenu();
    }
}