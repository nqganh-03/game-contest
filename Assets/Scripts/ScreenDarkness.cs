using UnityEngine;
using UnityEngine.UI;

public class ScreenDarkness : MonoBehaviour
{
    public Image darkOverlay;   // <-- THIS must exist

    public float darknessIncreaseRate = 0.1f;
    public float lightenAmount = 0.5f;

    private float currentAlpha = 0f;

    void Update()
    {
        currentAlpha += darknessIncreaseRate * Time.deltaTime;
        currentAlpha = Mathf.Clamp01(currentAlpha);

        darkOverlay.color = new Color(0, 0, 0, currentAlpha);
    }

    public void BrightenScreen()
    {
        currentAlpha -= lightenAmount;
        currentAlpha = Mathf.Clamp01(currentAlpha);
    }
}


