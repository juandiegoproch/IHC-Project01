using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
[RequireComponent(typeof(HorizontalLayoutGroup))]
public class PercentagePaddingHorizontalLayoutGroup : MonoBehaviour
{
    [Range(0f, 1f)]
    public float paddingPercentageLeft = 0f;
    [Range(0f, 1f)]
    public float paddingPercentageRight = 0f;
    [Range(0f, 1f)]
    public float paddingPercentageTop = 0f;
    [Range(0f, 1f)]
    public float paddingPercentageBottom = 0f;
    [Range(0f, 1f)]
    public float spacingPercentage = 0f;

    private RectTransform parentRectTransform;
    private HorizontalLayoutGroup layoutGroup;
    private bool initialized = false;

    private float lastParentWidth = -1f;
    private float lastParentHeight = -1f;

    void Awake()
    {
        layoutGroup = GetComponent<HorizontalLayoutGroup>();
        parentRectTransform = transform.parent as RectTransform;
        ApplyPercentagePaddingAndSpacing();
        initialized = true;
    }

    void Update()
    {
        if (initialized && parentRectTransform != null)
        {
            float currentWidth = parentRectTransform.rect.width;
            float currentHeight = parentRectTransform.rect.height;

            // Only recalculate if size or layout properties changed
            if (currentWidth != lastParentWidth || currentHeight != lastParentHeight)
            {
                ApplyPercentagePaddingAndSpacing();
                lastParentWidth = currentWidth;
                lastParentHeight = currentHeight;
            }
        }
    }

    void ApplyPercentagePaddingAndSpacing()
    {
        if (layoutGroup != null && parentRectTransform != null)
        {
            float parentWidth = parentRectTransform.rect.width;
            float parentHeight = parentRectTransform.rect.height;

            layoutGroup.padding.left = Mathf.RoundToInt(parentWidth * paddingPercentageLeft);
            layoutGroup.padding.right = Mathf.RoundToInt(parentWidth * paddingPercentageRight);
            layoutGroup.padding.top = Mathf.RoundToInt(parentHeight * paddingPercentageTop);
            layoutGroup.padding.bottom = Mathf.RoundToInt(parentHeight * paddingPercentageBottom);
            layoutGroup.spacing = parentWidth * spacingPercentage; // Spacing based on width for horizontal layout

            // Force the layout group to recalculate only when necessary
            LayoutRebuilder.MarkLayoutForRebuild(layoutGroup.GetComponent<RectTransform>());
        }
    }
}
