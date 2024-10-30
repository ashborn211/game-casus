using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class FillCanvas : MonoBehaviour
{
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Set the GameObject to fill the entire Canvas on start
        SetSizeToCanvas();
    }

    void Update()
    {
        // Continuously update the size to ensure it matches the Canvas size
        SetSizeToCanvas();
    }

    private void SetSizeToCanvas()
    {
        if (rectTransform != null)
        {
            // Get the parent Canvas RectTransform
            RectTransform canvasRectTransform = rectTransform.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            // Set the anchors to stretch to all corners of the parent RectTransform
            rectTransform.anchorMin = Vector2.zero;   // Bottom left corner
            rectTransform.anchorMax = Vector2.one;    // Top right corner

            // Set the size to match the Canvas size (optional, as anchors will take care of it)
            rectTransform.sizeDelta = Vector2.zero;    // Reset the sizeDelta since we're using anchors

            // Optional: Center the GameObject in the Canvas (not needed when stretching)
            rectTransform.anchoredPosition = Vector2.zero; // Center it in the Canvas
        }
    }
}
