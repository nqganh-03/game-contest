using UnityEngine;
using UnityEngine.UI;

public class DarknessOverlay : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public RawImage darknessImage; // RawImage for radial mask effect

    [Header("Darkness Settings")]
    public float maxViewRadius = 800f; // View radius at full time (in pixels)
    public float minViewRadius = 100f; // View radius at almost no time (in pixels)
    public Color darknessColor = Color.black;

    [Header("Time Reference")]
    public float maxTime = 30f; // Should match starting time

    [Header("Warning Pulse")]
    public bool enablePulse = true;
    public float pulseThreshold = 0.25f; // Pulse when below 25% time
    public float pulseSpeed = 3f;
    public float pulseAmount = 20f; // Pixels

    private Material darknessMaterial;
    private RectTransform imageRect;
    private Canvas canvas;

    void Start()
    {
        // Find player if not assigned
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        // Setup darkness image
        if (darknessImage != null)
        {
            imageRect = darknessImage.GetComponent<RectTransform>();
            canvas = darknessImage.canvas;

            // Create material with radial gradient shader
            CreateDarknessMaterial();
        }
        else
        {
            Debug.LogError("DarknessOverlay: No RawImage assigned!");
        }
    }

    void CreateDarknessMaterial()
    {
        // Create a simple material (we'll use sprite shader for now)
        // For better effect, you'd want a custom shader
        darknessMaterial = new Material(Shader.Find("UI/Default"));
        darknessImage.material = darknessMaterial;
        darknessImage.color = darknessColor;
    }

    void Update()
    {
        if (TimerManager.Instance == null || player == null || darknessImage == null) return;

        // Get time percentage
        float timeRemaining = TimerManager.Instance.timeRemaining;
        float timePercentage = Mathf.Clamp01(timeRemaining / maxTime);

        // Calculate current view radius
        float currentRadius = Mathf.Lerp(minViewRadius, maxViewRadius, timePercentage);

        // Add pulse effect when time is low
        if (enablePulse && timePercentage < pulseThreshold)
        {
            float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
            currentRadius += pulse;
        }

        // Update darkness effect
        UpdateDarknessVisual(timePercentage, currentRadius);
    }

    void UpdateDarknessVisual(float timePercentage, float radius)
    {
        // Fade in darkness as time runs out
        Color color = darknessColor;
        color.a = Mathf.Lerp(0f, 0.95f, 1f - timePercentage);
        darknessImage.color = color;

        // If you have a custom shader, you would update the radius here
        // For now, this creates a simple fade effect
    }
}