using UnityEngine;

public class LevelManage : MonoBehaviour
{
    public Transform playerLight;
    public Transform Aura;
    // ... (Giữ nguyên các biến public minSize, maxSize của bạn) ...
    public float minSizeL = 1f;
    public float maxSizeL = 3f;
    public float minSizeA = 0.5f;
    public float maxSizeA = 1f;

    public float pulseSpeed = 2f;
    public float pulseAmount = 0.1f;

    void Update()
    {
        // Quan trọng: Lấy thời gian từ GameManager
        if (GameManage.Instance != null)
        {
            float current = GameManage.Instance.currentTime;
            float total = GameManage.Instance.totalTime;

            float ratio = current / total; // Tính tỷ lệ

            // Logic cũ của bạn
            float baseScaleL = Mathf.Lerp(minSizeL, maxSizeL, ratio);
            float baseScaleA = Mathf.Lerp(minSizeA, maxSizeA, ratio);
            float breathing = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

            float finalScaleL = baseScaleL + breathing;
            float finalScaleA = baseScaleA + breathing;

            // Áp dụng
            if (playerLight != null) playerLight.localScale = new Vector3(finalScaleL, finalScaleL, 1f);
            if (Aura != null) Aura.localScale = new Vector3(finalScaleA, finalScaleA, 1f);
        }
    }
}