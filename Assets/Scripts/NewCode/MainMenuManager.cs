using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject levelSelectPanel; // Bảng chọn màn
    public GameObject mainButtonsPanel; // Bảng chứa nút Start/Exit...

    void Start()
    {
        // Khi game bắt đầu: Hiện menu chính, ẩn bảng chọn level
        mainButtonsPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // --- SỬA ĐỔI LOGIC ---

    public void OpenLevelSelect()
    {
        // Mở bảng chọn level -> Ẩn menu chính
        levelSelectPanel.SetActive(true);
        mainButtonsPanel.SetActive(false);
    }

    public void CloseLevelSelect()
    {
        // Đóng bảng chọn level -> Hiện lại menu chính
        levelSelectPanel.SetActive(false);
        mainButtonsPanel.SetActive(true);
    }

    public void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}