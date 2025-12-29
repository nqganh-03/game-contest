using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance { get; private set; }

    [Header("Time Settings")]
    public float timeRemaining = 60f;

    [Header("UI")]
    public TMP_Text timerText;
    public Slider timeSlider;

    private float maxTime;
    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Lưu thời gian ban đầu
        maxTime = timeRemaining;

        // Setup slider
        if (timeSlider != null)
        {
            timeSlider.minValue = 0f;
            timeSlider.maxValue = maxTime;
            timeSlider.value = maxTime;
            timeSlider.interactable = false;
        }
    }

    void Update()
    {
        if (isGameOver) return;

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            GameOver();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        // Update text
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"Time: {minutes:00}:{seconds:00}";
        }

        // Update slider
        if (timeSlider != null)
        {
            timeSlider.value = timeRemaining;

            // Đổi màu khi sắp hết giờ
            Image fillImage = timeSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = timeRemaining <= 5f ? Color.red : Color.green;
            }
        }
    }

    public void AddTime(float amount)
    {
        if (isGameOver) return;

        timeRemaining += amount;
        timeRemaining = Mathf.Clamp(timeRemaining, 0f, maxTime);
    }

    void GameOver()
    {
        isGameOver = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseGame();
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
