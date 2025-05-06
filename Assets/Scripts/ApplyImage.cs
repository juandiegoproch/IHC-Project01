using UnityEngine;
using UnityEngine.UI;

public class ApplyImage : MonoBehaviour
{
    public Texture2D imageToDisplay;
    public Renderer targetRenderer;

    private RectTransform rectTransform;
    private Vector3 normalScale = Vector3.one;
    private Vector3 selectedScale = new Vector3(1.1f, 1.1f, 1f);

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ApplyTheImage()
    {
        if (targetRenderer != null && imageToDisplay != null)
        {
            // Set the new texture on the quad
            targetRenderer.material.mainTexture = imageToDisplay;

            // Adjust quad scale to match image aspect ratio
            float aspect = (float)imageToDisplay.width / imageToDisplay.height;
            Transform quadTransform = targetRenderer.transform;
            Vector3 originalScale = Vector3.one; // Customize if needed
            quadTransform.localScale = new Vector3(originalScale.y * aspect, originalScale.y, 1f);

            // Scale this button up and reset others
            UpdateButtonScales();
        }
        else
        {
            Debug.LogError("Target Renderer or Image to Display not assigned.");
        }
    }

    private void UpdateButtonScales()
    {
        ApplyImage[] allButtons = FindObjectsOfType<ApplyImage>();
        foreach (var button in allButtons)
        {
            if (button.rectTransform == null)
                continue;

            if (button.imageToDisplay == targetRenderer.material.mainTexture)
                button.rectTransform.localScale = selectedScale;
            else
                button.rectTransform.localScale = normalScale;
        }
    }
}
