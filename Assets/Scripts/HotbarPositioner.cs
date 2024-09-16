using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class HotbarPositioner : MonoBehaviour
{
    public Vector2 offset = new Vector2(10f, 10f); // Offset from the bottom-left corner

    private RectTransform rectTransform;

    void Start()
    {
        // Get the RectTransform component of the hotbar
        rectTransform = GetComponent<RectTransform>();

        // Anchor the hotbar to the bottom-left corner
        rectTransform.anchorMin = new Vector2(0, 0); // Bottom-left
        rectTransform.anchorMax = new Vector2(0, 0); // Bottom-left
        rectTransform.pivot = new Vector2(0, 0);     // Set pivot to the bottom-left corner

        // Position the hotbar with the given offset
        rectTransform.anchoredPosition = offset;
    }
}
