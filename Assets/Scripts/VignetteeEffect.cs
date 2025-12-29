using UnityEngine;
using UnityEngine.UI;

public class VignetteEffect : MonoBehaviour
{
    [Header("References")]
    public Transform player; // The player transform to follow
    public Image vignetteImage; // Black overlay image

    [Header("Vignette Settings")]
    public float maxVignetteSize = 10f; // Size when time is full
    public float minVignetteSize = 1f; // Size when time is almost out
    public Color vignetteColor = Color.black; // Color of the darkness

    [Header("Animation")]
    public float pulseSpeed = 2f; // Speed of pulsing effect when low on time
    public float pulseAmount = 0.1f; // How much to pulse

    private Material vignetteMaterial;
    private Camera mainCamera;
    private float currentSize;

    void Start()
    {
        mainCamera = Camera.main;

        // Create vignette material if image is provided
        if (vignetteImage != null)
        {
            vignetteImage.color = vignetteColor;
        }

        // Find player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    void Update()
    {
        if (TimerManager.Instance == null || player == null) return;

        // Get time percentage (1 = full time, 0 = no time)
        float timePercentage = TimerManager.Instance.timeRemaining / 30f; // Adjust 30f to your max time
        timePercentage = Mathf.Clamp01(timePercentage);

        // Calculate vignette size based on time
        currentSize = Mathf.Lerp(minVignetteSize, maxVignetteSize, timePercentage);

        // Add pulse effect when low on time
        if (timePercentage < 0.3f)
        {
            float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            currentSize += pulse;
        }

        // Update vignette effect
        UpdateVignetteEffect(timePercentage);
    }

    void UpdateVignetteEffect(float timePercentage)
    {
        if (vignetteImage == null || mainCamera == null) return;

        // Convert player world position to screen position
        Vector3 screenPos = mainCamera.WorldToScreenPoint(player.position);

        // Convert to UI position
        RectTransform canvasRect = vignetteImage.canvas.GetComponent<RectTransform>();
        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            vignetteImage.canvas.worldCamera,
            out uiPos
        );

        // Set the radial gradient center (requires shader or material setup)
        // For now, we'll fade the entire screen based on time
        Color color = vignetteColor;
        color.a = Mathf.Lerp(0f, 1f, 1f - timePercentage); // Fade in as time runs out
        vignetteImage.color = color;
    }
}