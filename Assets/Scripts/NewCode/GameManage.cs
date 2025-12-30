using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance; // Singleton

    [Header("Cài đặt Game")]
    public float totalTime = 60f;
    public float currentTime;
    public bool isGameOver = false;

    [Header("Hệ thống Sao (QUAN TRỌNG)")]
    public int starsCollected = 0; // Đếm số sao đã ăn
    // Mảng chứa 3 cái ảnh ngôi sao trên UI WinPanel
    public GameObject[] winStars;

    [Header("UI References")]
    public Text timerText;
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject nextLevelButton;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        currentTime = totalTime;
        starsCollected = 0; // Reset sao về 0
        isGameOver = false;
        Time.timeScale = 1;

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        // Tắt hết hình ngôi sao trên bảng Win lúc bắt đầu
        if (winStars != null)
        {
            foreach (GameObject star in winStars)
            {
                if (star != null) star.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (isGameOver) return;

        currentTime -= Time.deltaTime;
        if (timerText != null) timerText.text = "Time: " + Mathf.Ceil(currentTime).ToString();

        if (currentTime <= 0)
        {
            currentTime = 0;
            LoseGame();
        }
    }

    // --- HÀM ĂN SAO ---
    public void CollectStar()
    {
        if (isGameOver) return;
        starsCollected++; // Tăng số lượng sao lên 1
        Debug.Log("Đã ăn sao! Tổng: " + starsCollected);
    }

    public void WinGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0;

        if (winPanel != null)
        {
            winPanel.SetActive(true);
            CheckLastLevel();

            // Hiện sao lên bảng vàng
            ShowStarsResult();
        }
    }

    // Hàm bật đèn các ngôi sao
    void ShowStarsResult()
    {
        if (winStars != null)
        {
            // Duyệt vòng lặp theo số sao đã ăn
            for (int i = 0; i < starsCollected; i++)
            {
                // Nếu ăn 2 sao -> bật star[0] và star[1]
                if (i < winStars.Length && winStars[i] != null)
                {
                    winStars[i].SetActive(true);
                }
            }
        }
    }

    // Các hàm phụ trợ khác giữ nguyên
    void CheckLastLevel()
    {
        int current = SceneManager.GetActiveScene().buildIndex;
        int last = SceneManager.sceneCountInBuildSettings - 1;
        if (current >= last && nextLevelButton != null) nextLevelButton.SetActive(false);
    }

    public void LoseGame()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0;
        if (losePanel != null) losePanel.SetActive(true);
    }

    public void AddTime(float amount)
    {
        if (!isGameOver) currentTime += amount;
    }

    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void NextLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void BackToMenu() => SceneManager.LoadScene(0);
}