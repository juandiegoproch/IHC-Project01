using UnityEngine;

[ExecuteAlways]
public class ForceAspectRatio : MonoBehaviour
{
    public float aspectRatio = 1f; // Set this to 1 for a 1:1 width-to-height ratio

    private RectTransform rectTransform;
    private float lastHeight;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        lastHeight = rectTransform.rect.height;  // Initialize with the current height
    }

    void Update()
    {
        if (rectTransform != null)
        {
            // Only update if the height has changed
            float currentHeight = rectTransform.rect.height;

            if (Mathf.Abs(currentHeight - lastHeight) > Mathf.Epsilon)
            {
                // Update width based on the height and the aspect ratio
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentHeight * aspectRatio);
                lastHeight = currentHeight;
            }
        }
    }
}
