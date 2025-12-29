using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public TMP_Text levelText;
    public GameObject[] starIcons; // 3 star icons for this level button

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(LoadLevel);
        }

        if (levelText != null)
        {
            levelText.text = $"Level {levelNumber}";
        }

        // Show stars earned for this level
        UpdateStarDisplay();
    }

    void UpdateStarDisplay()
    {
        if (starIcons == null || starIcons.Length == 0) return;

        // Get saved stars for this level
        int stars = GameManager.GetLevelStars(levelNumber);

        // Show/hide star icons based on earned stars
        for (int i = 0; i < starIcons.Length; i++)
        {
            if (starIcons[i] != null)
            {
                starIcons[i].SetActive(i < stars);
            }
        }
    }

    void LoadLevel()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadLevel(levelNumber);
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene($"Level{levelNumber}");
        }
    }
}